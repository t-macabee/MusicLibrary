using API.Data;
using API.DTOs.UpdateDTOs;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AlbumController : BaseAPIController
    {      
        private DataContext context;
        private IMapper mapper;
        private IUnitOfWork unitOfWork;

        public AlbumController(DataContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.context = context;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> GetAllAlbums()
        {
            var albums = await unitOfWork.AlbumRepository.GetAllAlbums();
            return Ok(mapper.Map<IEnumerable<AlbumDto>>(albums));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Album>> GetAlbumById(int id)
        {
            var result = await unitOfWork.AlbumRepository.GetAlbumByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("albumName")]
        public async Task<ActionResult<AlbumDto>> GetAlbumByName(string name)
        {
            var result = await unitOfWork.AlbumRepository.GetAlbumByNameAsync(name);
            return Ok(mapper.Map<AlbumDto>(result));
        }

        [HttpPost("create/{artistId}")]
        public async Task<ActionResult> CreateAlbumForArtist(int artistId, AlbumUpsertDto album)
        {
            var artist = await unitOfWork.ArtistRepository.GetArtistByIdAsync(artistId);

            if (artist == null)
            {
                return NotFound("Artist not found");
            }

            if (await AlbumExistsForArtist(album.AlbumName, artistId))
            {
                return BadRequest("Album already exists for this artist");
            }

            var newAlbum = mapper.Map<Album>(album);
            newAlbum.ArtistId = artistId;

            unitOfWork.AlbumRepository.CreateAlbum(newAlbum);

            if (await unitOfWork.Complete())
            {
                var albumDto = mapper.Map<AlbumDto>(newAlbum); // Use the newly created album for the response
                return Ok(albumDto);
            }

            return BadRequest("Failed to create new album");
        }

        [HttpPut("{artistId}/{albumId}")]
        public async Task<ActionResult> UpdateAlbum(int artistId, int albumId, AlbumUpsertDto update)
        {
            var artist = await unitOfWork.ArtistRepository.GetArtistByIdAsync(artistId);

            if (artist == null)
            {
                return NotFound("Artist not found");
            }

            var albumToUpdate = artist.Albums.FirstOrDefault(a => a.Id == albumId);

            if (albumToUpdate == null)
            {
                return BadRequest("Album not found for the artist");
            }

            mapper.Map(update, albumToUpdate);

            unitOfWork.AlbumRepository.UpdateAlbum(albumToUpdate);

            if (await unitOfWork.Complete())
            {
                return NoContent();
            }

            return BadRequest("Failed to update album");
        }

        [HttpDelete("{artistId}/{albumId}")]
        public async Task<ActionResult> DeleteAlbum(int artistId, int albumId)
        {
            var artist = await unitOfWork.ArtistRepository.GetArtistByIdAsync(artistId);

            if (artist == null)
            {
                return NotFound("Artist not found");
            }

            var albumToDelete = artist.Albums.FirstOrDefault(a => a.Id == albumId);

            if (albumToDelete == null)
            {
                return BadRequest("Album not found for the artist");
            }

            unitOfWork.AlbumRepository.DeleteAlbum(albumToDelete);

            if (await unitOfWork.Complete())
            {
                return Ok();
            }

            return BadRequest("Problem deleting the album");
        }

        private async Task<bool> AlbumExistsForArtist(string albumName, int artistId)
        {
            return await context.Albums
                .AnyAsync(album => album.ArtistId == artistId && album.AlbumName.ToLower() == albumName.ToLower());
        }
    }
}
