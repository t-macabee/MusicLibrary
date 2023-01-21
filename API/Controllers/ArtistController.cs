using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ArtistController : BaseApiController
    {
        private IArtistRepository _artistRepository;
        private IMapper _mapper;

        public ArtistController(IArtistRepository artistRepository, IMapper mapper)
        {
            _artistRepository = artistRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistDto>>> GetArtists()
        {
            var artists = await _artistRepository.GetArtistsAsync();
            return Ok(artists);
        }

        [HttpGet("{artistName}")]
        public async Task<ActionResult<ArtistDto>> GetArtistByName(string artistName)
        {
            return await _artistRepository.GetArtistByNameAsync(artistName);
        }


        [HttpGet("id")]
        public async Task<ActionResult<ArtistDto>> GetArtistById(int id)
        {
            return await _artistRepository.GetArtistByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddArtist(ArtistDto artist)
        {
            return await _artistRepository.AddNewArtist(artist);
        }

        [HttpPut("id")]
        public IActionResult UpdateArtist(int id, [FromBody]ArtistDto artistUpdate)
        {
            if (artistUpdate == null)
                throw new Exception("Field is empty!");

            if (id != artistUpdate.Id)
                throw new Exception("ID mismatch!");

            if (!_artistRepository.ArtistExists(id))
                throw new Exception("Artist not found!");

            var artistMap = _mapper.Map<Artist>(artistUpdate);

            _artistRepository.UpdateArtist(artistMap);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteArtist(int id)
        {
            if(!_artistRepository.ArtistExists(id))
                throw new Exception("Artist not found!");

            var artistToDelete = _artistRepository.GetArtistById(id);

            _artistRepository.DeleteArtist(artistToDelete);
            return NoContent();
        }
    }
}
