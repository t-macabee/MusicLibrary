using API.Entities;

namespace API.Interfaces
{
    public interface IAlbumRepository
    {
        void CreateAlbum(Album album);
        void UpdateAlbum(Album album);
        void DeleteAlbum(Album album);

        Task<Album> GetAlbumById(int albumId);
        Task<IEnumerable<Album>> GetAlbumByArtist(int artistId);
    }
}
