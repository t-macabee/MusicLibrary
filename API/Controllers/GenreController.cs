using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    public class GenreController : BaseApiController
    {
        private IGenreRepository genreRepository;
        private IMapper mapper;

        public GenreController(IGenreRepository genreRepository, IMapper mapper)
        {
            this.genreRepository = genreRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetGenres()
        {
            var result = mapper.Map<List<GenreDto>>(genreRepository.GetGenres());
            return Ok(result);
        }

        [HttpGet("id")]
        public IActionResult GetGenreById(int id)
        {
            var result = mapper.Map<GenreDto>(genreRepository.GetGenreById(id));
            return Ok(result);
        }

        [HttpGet("name")]
        public IActionResult GetGenreByName(string name)
        {
            var result = mapper.Map<GenreDto>(genreRepository.GetGenreByName(name));
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateGenre([FromBody]GenreDto genreCreate)
        {
            if(genreCreate == null)
                throw new Exception("Field is empty!");

            var genre = genreRepository.GetGenres()
                .Where(x => x.GenreName.ToLower() == genreCreate.GenreName.ToLower())
                .FirstOrDefault();

            if(genre != null)
                throw new Exception("Genre already exists!");

            var genreMap = mapper.Map<Genre>(genreCreate);

            genreRepository.CreateGenre(genreMap);
            return Ok("Success!");
        }

        [HttpPut("id")]
        public IActionResult UpdateGenre(int id, [FromBody]GenreDto genreUpdate)
        {
            if (genreUpdate == null)
                throw new Exception("Field is empty!");

            if (id != genreUpdate.Id)
                throw new Exception("ID mismatch!");

            if (!genreRepository.GenreExists(id))
                throw new Exception("Genre not found!");

            var genreMap = mapper.Map<Genre>(genreUpdate);

            genreRepository.UpdateGenre(genreMap);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteGenre(int id)
        {
            if (!genreRepository.GenreExists(id))
                throw new Exception("Genre not found!");

            var genreToDelete = genreRepository.GetGenreById(id);

            genreRepository.DeleteGenre(genreToDelete);
            return NoContent();
        }

    }
}
