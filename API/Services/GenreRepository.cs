using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class GenreRepository : IGenreRepository
    {
        private DataContext context;
        private IMapper mapper;

        public GenreRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }       

        public async Task<IEnumerable<GenreDto>> GetAllGenres()
        {
            var result = await context.Genres.ToListAsync();
            return mapper.Map<IEnumerable<GenreDto>>(result);
        }

        public async Task<Genre> GetGenreById(int id)
        {
            return await context.Genres.FindAsync(id);            
        }             

        public async Task<GenreDto> GetGenreByName(string name)
        {
            var result = await context.Genres.SingleOrDefaultAsync(x => x.GenreName.ToLower() == name.ToLower());
            return mapper.Map<GenreDto>(result);
        }       
    }
}
