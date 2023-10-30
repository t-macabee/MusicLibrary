using API.DTOs;
using API.Entities;
using System.Collections;

namespace API.Interfaces
{
    public interface IGenreRepository
    {
        void AddGenre(Genre genre);
        void RemoveGenre(Genre genre);
        Task<GenreDto> GetGenreByNameAsync(string name);
        Task<Genre> GetGenreAsyncByIdAsync(int id);
        Task<IEnumerable<GenreDto>> GetAllGenresAsync();
    }
}
