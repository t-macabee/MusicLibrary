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
    public class TrackController : BaseAPIController
    {
        private DataContext context;
        private IMapper mapper;
        private IUnitOfWork unitOfWork;

        public TrackController(DataContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.context = context;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrackDto>>> GetAllTracks()
        {
            var track = await unitOfWork.TrackRepository.GetAllTracks();
            return Ok(mapper.Map<IEnumerable<TrackDto>>(track));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrackDto>> GetTrackById(int id)
        {
            var result = await unitOfWork.TrackRepository.GetTrackById(id);
            return Ok(mapper.Map<TrackDto>(result));
        }

        [HttpGet("trackName")]
        public async Task<ActionResult<TrackDto>> GetTrackByName(string name)
        {
            var result = await unitOfWork.TrackRepository.GetTrackByName(name);
            return Ok(mapper.Map<TrackDto>(result));
        }

        [HttpGet("tracksByAlbum/{albumId}")]
        public async Task<ActionResult<IEnumerable<TrackDto>>> GetTracksByAlbum(int albumId)
        {
            var tracks = await unitOfWork.TrackRepository.GetTracksByAlbum(albumId);

            if (tracks == null)
            {
                return Ok(new List<TrackDto>());
            }

            return Ok(tracks);
        }


        [HttpPost("{albumId}/tracks")]
        public async Task<ActionResult> AddTrackToAlbum(int albumId, TrackUpsertDto trackDto)
        {
            var album = await unitOfWork.AlbumRepository.GetAlbumById(albumId);

            if (album == null)
            {
                return NotFound("Album not found");
            }

            if (album.Tracks == null)
            {
                album.Tracks = new List<Track>();
            }

            if (await TrackExistsForAlbum(trackDto.TrackName, albumId))
                return BadRequest("Track already exists");

            var newTrack = mapper.Map<Track>(trackDto);

            album.Tracks.Add(newTrack);

            unitOfWork.TrackRepository.CreateTrack(newTrack);

            if (await unitOfWork.Complete())
            {
                return Ok(mapper.Map<TrackDto>(newTrack));
            }

            return BadRequest("Failed to add track to album");
        }

        [HttpPut("tracks/{trackId}")]
        public async Task<ActionResult> UpdateTrack(int trackId, TrackUpsertDto updatedTrackDto)
        {
            var track = await unitOfWork.TrackRepository.GetTrackById(trackId);

            if (track == null)
            {
                return NotFound("Track not found");
            }

            mapper.Map(updatedTrackDto, track);

            unitOfWork.TrackRepository.UpdateTrack(track);

            if (await unitOfWork.Complete())
            {
                return Ok(mapper.Map<TrackDto>(track));
            }

            return BadRequest("Failed to update the track");
        }

        [HttpDelete("tracks/{trackId}")]
        public async Task<ActionResult> DeleteTrack(int trackId)
        {
            var track = await unitOfWork.TrackRepository.GetTrackById(trackId);

            if (track == null)
            {
                return NotFound("Track not found");
            }

            unitOfWork.TrackRepository.DeleteTrack(track);

            if (await unitOfWork.Complete())
            {
                return Ok( new { message = "Track deleted successfully" } );
            }

            return BadRequest("Failed to delete the track");
        }   

        private async Task<bool> TrackExistsForAlbum(string trackName, int albumId)
        {
            return await context.Tracks
                .AnyAsync(track => track.AlbumId == albumId && track.TrackName.ToLower() == trackName.ToLower());
        }
    }
}
