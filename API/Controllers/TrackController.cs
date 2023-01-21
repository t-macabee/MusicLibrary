using API.Data;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class TrackController : BaseApiController
    {
        private readonly ITrackRepository _trackRepository;
        private IMapper _mapper;

        public TrackController(ITrackRepository trackRepository, IMapper mapper)
        {
            _trackRepository = trackRepository;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrackDto>>> GetTracks()
        {
            var tracks = await _trackRepository.GetTracksAsync();
            return Ok(tracks);
        }

        [HttpGet("trackName")]
        public async Task<ActionResult<TrackDto>> GetTrack(string trackName)
        {
            return await _trackRepository.GetTrackByNameAsync(trackName);
        }

        [HttpGet("track-by-artist")]
        public IActionResult GetTrackByArtist(int artistId)
        {
            var result = _mapper.Map<List<TrackDto>>(_trackRepository.GetTrackByArtist(artistId));
            return Ok(result);
        }

        [HttpGet("track-by-genre")]
        public IActionResult GetTrackByGenre(int genreId)
        {
            var result = _mapper.Map<List<TrackDto>>(_trackRepository.GetTrackByGenre(genreId));
            return Ok(result);
        }


        [HttpPost("add-new-track")]
        public async Task<ActionResult<bool>> AddNewTrack([FromQuery]int artistId, [FromQuery]int genreId, TrackDto trackCreate)
        {
            if(trackCreate == null)
                throw new Exception("Field is empty!");

            var tracks = _trackRepository.GetTracks()
                .Where(x => x.TrackName.ToLower() == trackCreate.TrackName.ToLower())
                .FirstOrDefault();

            if(tracks != null)
                throw new Exception("Track already exists!");

            var trackMap = _mapper.Map<Track>(trackCreate);  
            
       

            return await _trackRepository.AddNewTrack(trackMap, artistId, genreId);
        }


        //Track track, int artistId, int genreId
        [HttpPut("id")]
        public IActionResult UpdateTrack(int id, [FromQuery]int artistId, [FromQuery]int genreId, [FromBody]TrackDto trackUpdate)
        {
            if (trackUpdate == null)
                throw new Exception("Field is empty!");

            if (id != trackUpdate.Id)
                throw new Exception("ID mismatch!");

            if (!_trackRepository.TrackExists(id))
                throw new Exception("Track not found!");

            var trackMap = _mapper.Map<Track>(trackUpdate);

            _trackRepository.UpdateTrack(trackMap, artistId, genreId);
                

            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteTrack(int id)
        {
            if (!_trackRepository.TrackExists(id))
                throw new Exception("Track not found!");
            
            var tracktoDelete = _trackRepository.GetTrackById(id);

            _trackRepository.DeleteTrack(tracktoDelete);                

            return NoContent();
        }
    }
}
