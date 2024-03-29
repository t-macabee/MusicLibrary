﻿using API.Data;
using API.DTOs.UpdateDTOs;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Services;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<AlbumDto>> GetAlbumById(int id)
        {
            var result = await unitOfWork.AlbumRepository.GetAlbumById(id);
            return Ok(mapper.Map<AlbumDto>(result));
        }

        [HttpGet("albumsByArtist/{artistId}")]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> GetAlbumsByArtist(int artistId)
        {
            var albums = await unitOfWork.AlbumRepository.GetAlbumByArtist(artistId);

            if (albums == null || !albums.Any())
            {
                return NotFound("No albums found for the specified artist.");
            }

            return Ok(mapper.Map<IEnumerable<AlbumDto>>(albums));
        }

        [HttpPost("create/{artistId}")]
        public async Task<ActionResult> CreateAlbumForArtist(int artistId, AlbumUpsertDto album)
        {
            var artist = await unitOfWork.ArtistRepository.GetArtistById(artistId);

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
                var albumDto = mapper.Map<AlbumDto>(newAlbum);
                return Ok(albumDto);
            }

            return BadRequest("Failed to create new album");
        }

        [HttpPut("{artistId}/{albumId}")]
        public async Task<ActionResult> UpdateAlbum(int artistId, int albumId, AlbumUpsertDto update)
        {
            var artist = await unitOfWork.ArtistRepository.GetArtistById(artistId);

            if (artist == null)
            {
                return NotFound("Artist not found");
            }

            var albumToUpdate = await unitOfWork.AlbumRepository.GetAlbumById(albumId);

            if (albumToUpdate == null)
            {
                return BadRequest("Album not found for the artist");
            }

            if (update == null)
            {
                return BadRequest("Update data is missing");
            }

            mapper.Map(update, albumToUpdate);

            albumToUpdate.Artist.ArtistName = artist.ArtistName;

            unitOfWork.AlbumRepository.UpdateAlbum(albumToUpdate);

            if (await unitOfWork.Complete())
            {
                return Ok(mapper.Map<AlbumDto>(albumToUpdate));
            }

            return BadRequest("Failed to update album");
        }


        [HttpDelete("{artistId}/{albumId}")]
        public async Task<ActionResult> DeleteAlbum(int artistId, int albumId)
        {
            var artist = await unitOfWork.ArtistRepository.GetArtistById(artistId);

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
