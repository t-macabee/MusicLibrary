using API.Entities;

namespace API.Interfaces
{
    public interface IPlaylistRepository
    {
        void CreatePlaylist(Playlist playlist);
        void UpdatePlaylist(Playlist playlist);
        void DeletePlaylist(Playlist playlist);



        Task<IEnumerable<Playlist>> GetAllPlaylists();
        Task<IEnumerable<Playlist>> GetAllPlaylistsByUserAsync(int userId);
        Task<Playlist> GetPlaylistByIdAsync(int id);
        Task<Playlist> GetPlaylistByNameAsync(string name);


        Task AddTrackToPlaylist(int playlistId, int trackId);
        Task RemoveTrackFromPlaylist(int playlistId, int trackId);
    }
}
