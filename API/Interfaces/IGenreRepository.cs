using API.DTOs;
using API.Entities;
using System.Collections;

namespace API.Interfaces
{
    public interface IGenreRepository
    {
        Task<GenreDto> GetGenreByName(string name);
        Task<Genre> GetGenreById(int id);
        Task<IEnumerable<GenreDto>> GetAllGenres();
    }
}
