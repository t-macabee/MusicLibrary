using API.Data;
using API.DTO;
using API.DTOs;
using API.DTOs.UpdateDTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class GenreController : BaseAPIController
    {
        private DataContext context;
        private IMapper mapper;
        private IUnitOfWork unitOfWork;

        public GenreController(DataContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.context = context;            
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<GenreDto>> GetGenres()
        {
            var result = await unitOfWork.GenreRepository.GetAllGenresAsync();
            
            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<ActionResult<GenreDto>> GetGenreById(int id)
        {
            var result = await unitOfWork.GenreRepository.GetGenreAsyncByIdAsync(id);

            return Ok(result);
        }

        [HttpGet("genre-name")]
        public async Task<ActionResult<GenreDto>> GetGenreByName(string name)
        {
            var result = await unitOfWork.GenreRepository.GetGenreByNameAsync(name);

            return Ok(result);
        }

        [HttpPost("new-genre")]
        public async Task<ActionResult<GenreDto>> CreateGenre(GenreUpsertDto genre)
        {
            if (await GenreExists(genre.GenreName))
                return BadRequest("Genre already exists!");

            var newGenre = mapper.Map<Genre>(genre);

            unitOfWork.GenreRepository.AddGenre(newGenre);

            if (await unitOfWork.Complete())
                return Ok(mapper.Map<GenreDto>(newGenre));

            return BadRequest("Failed to create new genre");
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteGenre(int id)
        {
            var genre = await unitOfWork.GenreRepository.GetGenreAsyncByIdAsync(id);
            
            if(genre == null)
                return BadRequest("Genre doesn't exist");

            unitOfWork.GenreRepository.RemoveGenre(genre);

            if (await unitOfWork.Complete())
                return Ok();

            return BadRequest("Problem deleting the genre");
        }   


        private async Task<bool> GenreExists(string genre)
        {
            return await context.Genres.AnyAsync(x => x.GenreName.ToLower() == genre.ToLower());
        }
    }
}
