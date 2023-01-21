using API.Entities;
using System.Collections.Generic;

namespace API.Interfaces
{
    public interface IAlbumRepository 
    {
        ICollection<Album> GetAllAlbums();
        Album GetAlbumById(int id);
        Album GetAlbumByName(string name);
        void AddTrackToAlbum(int albumId, int trackId);
        ICollection<Track> GetAllTracksByAlbum(int albumId);
        void CreateAlbum(Album album, int trackId);
        void UpdateAlbum(Album album);
        void DeleteAlbum(Album album);
        bool AlbumExists(int id);
    }
}
