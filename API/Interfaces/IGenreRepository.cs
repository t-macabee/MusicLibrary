using API.DTOs;

namespace API.Interfaces
{
    public interface IGenreRepository
    {
        Task<IEnumerable<GenreDto>> GetAllGenres();
    }
}
