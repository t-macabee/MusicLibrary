using API.Data;
using API.DTOs;
using API.DTOs.UpdateDTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ArtistController : BaseAPIController
    {
        private DataContext context;
        private IMapper mapper;
        private IUnitOfWork unitOfWork;
        private IPhotoService photoService;

        public ArtistController(DataContext context, IMapper mapper, IUnitOfWork unitOfWork, IPhotoService photoService)
        {
            this.context = context;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.photoService = photoService;
        }

        [HttpGet]
        public async Task<ActionResult<ArtistDto>> GetAllArtists()
        {
            var result = await unitOfWork.ArtistRepository.GetAllArtistsAsync();
            return Ok(mapper.Map<IEnumerable<ArtistDto>>(result));
        }

        [HttpGet("id")]
        public async Task<ActionResult<Artist>> GetArtistById(int id)
        {
            var result = await unitOfWork.ArtistRepository.GetArtistByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("artistName", Name = "GetArtist")]
        public async Task<ActionResult<ArtistDto>> GetArtistByName(string name)
        {
            var result = await unitOfWork.ArtistRepository.GetArtistByNameAsync(name);
            return Ok(mapper.Map<ArtistDto>(result));
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewArtist(ArtistUpsertDto artist)
        {
            if (await ArtistExists(artist.ArtistName))
                return BadRequest("Artist already exists!");

            var newArtist = mapper.Map<Artist>(artist);

            unitOfWork.ArtistRepository.CreateArtist(newArtist);

            if (await unitOfWork.Complete())
                return Ok(mapper.Map<ArtistDto>(newArtist));

            return BadRequest("Failed to create new artist");
        }

        [HttpPut("{artistID}")]
        public async Task<ActionResult> UpdateArtist(int artistID, ArtistUpsertDto update)
        {
            var artist = await unitOfWork.ArtistRepository.GetArtistByIdAsync(artistID);

            if (artist == null)
            {
                return NotFound("Artist not found");
            }

            mapper.Map(update, artist);

            unitOfWork.ArtistRepository.UpdateArtist(artist);

            if (await unitOfWork.Complete())
                return NoContent();

            return BadRequest("Failed to update artist");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArtist(int id)
        {
            var artist = await unitOfWork.ArtistRepository.GetArtistByIdAsync(id);            

            if (artist == null)
                return BadRequest("Artist doesn't exist");
            
            unitOfWork.ArtistRepository.DeleteArtist(artist);

            if (await unitOfWork.Complete())
                return Ok();

            return BadRequest("Problem deleting the artist");
        }

        private async Task<bool> ArtistExists(string artist)
        {
            return await context.Artists.AnyAsync(x => x.ArtistName.ToLower() == artist.ToLower());
        }

        //------------------------------------------------------------------------------------------

        [HttpPost("add-artist-photo")]
        public async Task<ActionResult<ArtistPhotoDto>> AddPhoto(int artistId, IFormFile file)
        {
            var artist = await unitOfWork.ArtistRepository.GetArtistByIdAsync(artistId);

            var result = await photoService.AddPhotoAsync(file);

            if (result.Error != null)
                return BadRequest(result.Error.Message);

            var photo = new ArtistPhoto
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if (artist.ArtistPhotos.Count == 0)
            {
                photo.IsMain = true;
            }

            artist.ArtistPhotos.Add(photo);

            if (await unitOfWork.Complete())
            {
                return CreatedAtRoute("GetArtist", new { artistName = artist.ArtistName }, mapper.Map<ArtistPhotoDto>(photo));
            }

            return BadRequest("Problem adding photo");

        }
    }
}
