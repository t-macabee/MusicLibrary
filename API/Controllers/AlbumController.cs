using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    public class AlbumController : BaseApiController
    {
        public IAlbumRepository albumRepository;
        public ITrackRepository trackRepository;
        public IMapper mapper;

        public AlbumController(IAlbumRepository albumRepository, ITrackRepository trackRepository, IMapper mapper)
        {
            this.albumRepository = albumRepository;
            this.trackRepository = trackRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllAlbums()
        {
            var result = mapper.Map<List<AlbumDto>>(albumRepository.GetAllAlbums());
            return Ok(result);
        }

        [HttpGet("id")]
        public IActionResult GetAlbumById(int id)
        {
            var result = mapper.Map<AlbumDto>(albumRepository.GetAlbumById(id));
            return Ok(result);
        }

        [HttpGet("name")]
        public IActionResult GetAlbumByName(string name)
        {
            var result = mapper.Map<AlbumDto>(albumRepository.GetAlbumByName(name));
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateAlbum([FromQuery] int trackId, [FromBody] AlbumDto albumCreate)
        {
            if (albumCreate == null)
                throw new Exception("Field is empty!");

            var albumCheck = albumRepository.GetAlbumByName(albumCreate.AlbumName);

            if (albumCheck != null)
                throw new Exception("Album already exists!");

            var albumMap = mapper.Map<Album>(albumCreate);

            albumRepository.CreateAlbum(albumMap, trackId);

            return Ok("Success!");
        }

        [HttpPost("album-track")]
        public IActionResult AddTrackToAlbum([FromQuery] int albumId, int trackId)
        {
            var albumCheck = albumRepository.AlbumExists(albumId);
            var trackCheck = trackRepository.TrackExists(trackId);

            if (albumCheck == null)
                throw new Exception("Album doesn't exists!");

            if (trackCheck == null)
                throw new Exception("Track doesn't exists!");

            albumRepository.AddTrackToAlbum(albumId, trackId);
            return Ok("Success");
        }

        [HttpPost("all-tracks-by-album")]
        public IActionResult GetAllTracksByAlbum(int albumId)
        {
            var albumCheck = albumRepository.AlbumExists(albumId);
            if (albumCheck == null)
                throw new Exception("Album doesn't exists!");

            var result = albumRepository.GetAllTracksByAlbum(albumId);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult DeleteAlbum(int albumId)
        {
            if (!albumRepository.AlbumExists(albumId))
                throw new Exception("Album doesn't exist");

            var albumtoDelete = albumRepository.GetAlbumById(albumId);

            albumRepository.DeleteAlbum(albumtoDelete);

            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateAlbum(int albumId, [FromBody] AlbumDto updateAlbum)
        {
            if (updateAlbum == null)
                throw new Exception("Field is empty!");

            if (albumId != updateAlbum.Id)
                throw new Exception("ID mismatch!");

            if (!albumRepository.AlbumExists(albumId))
                throw new Exception("Album not found!");

            var albumMap = mapper.Map<Album>(updateAlbum);

            albumRepository.UpdateAlbum(albumMap);

            return Ok("Success!");
        }
    }
}

