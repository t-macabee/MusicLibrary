using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    public class PlaylistController : BaseApiController
    {
        public IPlaylistRepository playlistRepository;
        public ITrackRepository trackRepository;
        public IMapper mapper;

        public PlaylistController(IPlaylistRepository playlistRepository, ITrackRepository trackRepository, IMapper mapper)
        {
            this.playlistRepository = playlistRepository;
            this.trackRepository = trackRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllPlaylists()
        {
            var result = mapper.Map<List<PlaylistDto>>(playlistRepository.GetAllPlaylists());
            return Ok(result);
        }

        [HttpGet("playlist-id")]
        public IActionResult GetPlaylistById(int id) 
        {
            var result = mapper.Map<PlaylistDto>(playlistRepository.GetPlaylistById(id));
            return Ok(result);
        }        

        [HttpPost]
        public IActionResult CreatePlaylist([FromBody]PlaylistDto playlistCreate)
        {
            if (playlistCreate == null)
                throw new Exception("Field is empty");

            var playlist = playlistRepository.GetAllPlaylists().Where(x => x.PlaylistName.ToLower() == playlistCreate.PlaylistName.ToLower()).FirstOrDefault();
           
            if(playlist != null)
                throw new Exception("Playlist already exists");

            var playlistMap = mapper.Map<Playlist>(playlistCreate);

            playlistRepository.CreatePlaylist(playlistMap);

            return Ok("Success!");

        }

        [HttpPost("add-track-to-playlist")]
        public IActionResult AddTrackToPlaylist([FromQuery]int playlistId, [FromQuery]int trackId)
        {
            var existingPlaylist = playlistRepository.GetPlaylistById(playlistId);
            var existingTrack = trackRepository.GetTrackById(trackId);

            if (existingPlaylist == null)
                throw new Exception("Playlist doesn't exist!");
            if (existingTrack == null)
                throw new Exception("Track doesn't exist!");

            playlistRepository.AddTrackToPlaylist(playlistId, trackId);
            return Ok("Success!");
        }

        [HttpDelete("remove-track-from-playlist")]
        public IActionResult RemoveTrackFromPlaylist([FromQuery] int playlistId, [FromQuery] int trackId)
        {
            var existingPlaylist = playlistRepository.GetPlaylistById(playlistId);
            var existingTrack = trackRepository.GetTrackById(trackId);

            if (existingPlaylist == null)
                throw new Exception("Playlist doesn't exist!");
            if (existingTrack == null)
                throw new Exception("Track doesn't exist!");

            playlistRepository.DeleteTrackFromPlaylist(playlistId, trackId);
            return Ok("Success!");
        }

        [HttpDelete("playlist-id-delete")]
        public IActionResult DeletePlaylist(int id) 
        {            
            if (!playlistRepository.PlaylistExists(id))
                throw new Exception("Playlist doesn't exist");

            var playlistToDelete = playlistRepository.GetPlaylistById(id);

            playlistRepository.DeletePlaylist(playlistToDelete);

            return NoContent();
        }
    }
}
