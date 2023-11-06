using API.Entities;

namespace API.Interfaces
{
    public interface IAlbumRepository
    {
        void CreateAlbum(Album album);
        void UpdateAlbum(Album album);
        void DeleteAlbum(Album album);

        Task<IEnumerable<Album>> GetAllAlbums();
        Task<Album> GetAlbumByIdAsync(int albumId);
        Task<Album> GetAlbumByNameAsync(string albumName);
    }
}
