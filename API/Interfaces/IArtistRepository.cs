using API.DTOs;
using API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IArtistRepository
    {
        void UpdateArtist(Artist artist);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<ArtistDto>> GetArtistsAsync();
        Task<ArtistDto> GetArtistByIdAsync(int id);
        Task<ArtistDto> GetArtistByNameAsync(string name);
        Task<bool> AddNewArtist(ArtistDto artist);
        void DeleteArtist(Artist artist);

        bool ArtistExists(int id);
        Artist GetArtistById(int id);
    }
}
