using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IArtistRepository
    {
        void CreateArtist(Artist artist);
        void UpdateArtist(Artist artist);
        void DeleteArtist(Artist artist);

        Task<IEnumerable<Artist>> GetAllArtistsAsync();
        Task<IEnumerable<Artist>>GetArtistsByGenre(string name);
        Task<Artist> GetArtistByIdAsync(int id);
        Task<Artist> GetArtistByNameAsync(string name);
    }
}
