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
    public class PlaylistController : BaseAPIController
    {
        private DataContext context;
        private IMapper mapper;
        private IUnitOfWork unitOfWork;

        public PlaylistController(DataContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.context = context;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }       

        [HttpGet("{id}")]
        public async Task<ActionResult<PlaylistDto>> GetPlaylistById(int id)
        {
            var playlist = await unitOfWork.PlaylistRepository.GetPlaylistById(id);
            return Ok(mapper.Map<PlaylistDto>(playlist));
        }

        [HttpGet("{userId}/playlists")]
        public async Task<ActionResult<IEnumerable<PlaylistDto>>> GetPlaylistsByUser(int userId)
        {
            var user = await unitOfWork.UserRepository.GetUserById(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var playlists = await unitOfWork.PlaylistRepository.GetAllPlaylistsByUser(userId);
            var playlistDtos = mapper.Map<IEnumerable<PlaylistDto>>(playlists);

            return Ok(playlistDtos);
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewPlaylist(int userId, PlaylistUpsertDto playlistDto)
        {
            var user = await unitOfWork.UserRepository.GetUserById(userId);

            if (user == null)
                return BadRequest("User does not exist!");

            if (await PlaylistExistsForUser(userId, playlistDto.PlaylistName))
            {
                return BadRequest("A playlist with the same name already exists for the user.");
            }

            var newPlaylist = mapper.Map<Playlist>(playlistDto);

            newPlaylist.AppUserId = userId;

            unitOfWork.PlaylistRepository.CreatePlaylist(newPlaylist);

            if (await unitOfWork.Complete())
            {
                return Ok(mapper.Map<PlaylistDto>(newPlaylist));
            }

            return BadRequest("Failed to create a new playlist.");
        }

        [HttpPut("{playlistId}")]
        public async Task<ActionResult> UpdatePlaylist(int playlistId, PlaylistUpsertDto playlistDto)
        {
            var playlist = await unitOfWork.PlaylistRepository.GetPlaylistById(playlistId);

            if (playlist == null)
            {
                return NotFound("Playlist not found");
            }

            mapper.Map(playlistDto, playlist);

            unitOfWork.PlaylistRepository.UpdatePlaylist(playlist);
            
            if (await unitOfWork.Complete())
            {
                return Ok(mapper.Map<PlaylistDto>(playlist));
            }

            return BadRequest("Failed to update the playlist");
        }

        [HttpDelete("{userId}/playlists/{playlistId}")]
        public async Task<ActionResult> DeletePlaylist(int userId, int playlistId)
        {
            var user = await unitOfWork.UserRepository.GetUserById(userId);

            if (user == null)
            {
                return BadRequest("User does not exist!");
            }

            var playlist = user.Playlists.FirstOrDefault(x => x.Id == playlistId);

            if (playlist == null)
            {
                return NotFound("Playlist not found for the user.");
            }

            user.Playlists.Remove(playlist);

            if (await unitOfWork.Complete())
            {
                return Ok(new { message = "Playlist removed from the user." });
            }

            return BadRequest("Failed to remove playlist from the user.");
        }

        [HttpPost("{playlistId}/tracks/{trackId}")]
        public async Task<ActionResult> AddTrackToPlaylist(int playlistId, int trackId)
        {
            var playlist = await unitOfWork.PlaylistRepository.GetPlaylistById(playlistId);
            var track = await unitOfWork.TrackRepository.GetTrackById(trackId);

            if (playlist == null || track == null)
            {
                return NotFound("Playlist or track not found");
            }

            if (playlist.PlaylistTracks == null)
            {
                playlist.PlaylistTracks = new List<PlaylistTrack>();
            }

            if (playlist.PlaylistTracks.Any(pt => pt.TrackId == track.Id))
            {
                return BadRequest("Track already exists in the playlist");
            }

            playlist.PlaylistTracks.Add(new PlaylistTrack { PlaylistId = playlist.Id, TrackId = track.Id });

            if (await unitOfWork.Complete())
            {
                return Ok(new { message = "Track added to the playlist" });
            }

            return BadRequest("Failed to add track to the playlist");
        }

        [HttpDelete("{playlistId}/tracks/{trackId}")]
        public async Task<ActionResult> RemoveTrackFromPlaylist(int playlistId, int trackId)
        {
            var playlist = await unitOfWork.PlaylistRepository.GetPlaylistById(playlistId);
            var track = await unitOfWork.TrackRepository.GetTrackById(trackId);

            if (playlist == null || track == null)
            {
                return NotFound("Playlist or track not found");
            }

            if (playlist.PlaylistTracks == null)
            {
                playlist.PlaylistTracks = new List<PlaylistTrack>();
            }

            var playlistTrackToRemove = playlist.PlaylistTracks.FirstOrDefault(pt => pt.TrackId == track.Id);

            if (playlistTrackToRemove != null)
            {
                playlist.PlaylistTracks.Remove(playlistTrackToRemove);

                if (await unitOfWork.Complete())
                {
                    return Ok(new { message = "Track removed from the playlist" });
                }

                return BadRequest("Failed to remove track from the playlist");
            }

            return BadRequest("Track not found in the playlist");
        }

        private async Task<bool> PlaylistExistsForUser(int userId, string playlistName)
        {
            return await context.Playlists
                .AnyAsync(playlist => playlist.PlaylistName.ToLower() == playlistName && playlist.AppUserId == userId);
        }
    }
}
