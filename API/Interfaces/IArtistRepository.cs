using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IArtistRepository
    {
        void CreateArtist(Artist artist);
        void UpdateArtist(Artist artist);
        void DeleteArtist(Artist artist);

        Task<IEnumerable<Artist>> GetAllArtists();
        Task<Artist> GetArtistById(int id);
    }
}
