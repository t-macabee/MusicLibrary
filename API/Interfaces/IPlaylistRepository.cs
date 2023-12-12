using API.Entities;

namespace API.Interfaces
{
    public interface IPlaylistRepository
    {
        void CreatePlaylist(Playlist playlist);
        void UpdatePlaylist(Playlist playlist);
        void DeletePlaylist(Playlist playlist);

        Task<IEnumerable<Playlist>> GetAllPlaylists();
        Task<IEnumerable<Playlist>> GetAllPlaylistsByUser(int userId);
        Task<Playlist> GetPlaylistById(int id);
        Task<Playlist> GetPlaylistByName(string name);

        Task AddTrackToPlaylist(int playlistId, int trackId);
        Task RemoveTrackFromPlaylist(int playlistId, int trackId);
    }
}
