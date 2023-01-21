using API.Entities;
using Microsoft.JSInterop;
using System.Collections;
using System.Collections.Generic;

namespace API.Interfaces
{
    public interface IPlaylistRepository
    {
        void CreatePlaylist(Playlist playlist);
        void DeletePlaylist(Playlist playlist);
        void AddTrackToPlaylist(int playlistId, int trackId);
        void DeleteTrackFromPlaylist(int playlistId, int trackId);
        ICollection<Track> GetTracksFromPlaylist(int playlistId);
        Playlist GetPlaylistById(int id);
        ICollection<Playlist> GetAllPlaylists();
        bool PlaylistExists(int id);
    }
}
