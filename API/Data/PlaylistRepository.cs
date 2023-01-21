using API.Entities;
using API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Data
{
    public class PlaylistRepository : IPlaylistRepository
    {
        public DataContext context { get; set; }
        public PlaylistRepository(DataContext context)
        {
            this.context = context;
        }

        public void CreatePlaylist(Playlist playlist)
        {
            context.Add(playlist);
            context.SaveChanges();
        }

        public void DeletePlaylist(Playlist playlist)
        {
            context.Remove(playlist);
            context.SaveChanges();
        }

        public void AddTrackToPlaylist(int playlistId, int trackId)
        {
            var existingPlaylist = context.Playlists.SingleOrDefault(x => x.Id == playlistId);
            var existingTrack = context.Tracks.SingleOrDefault(x => x.Id == trackId);

            var newAddition = new PlaylistTrack()
            {
                Playlist = existingPlaylist,
                Track = existingTrack
            };

            context.Add(newAddition);
            context.SaveChanges();
        }

        public void DeleteTrackFromPlaylist(int playlistId, int trackId)
        {            
            var itemToDelete = context.PlaylistTracks.Where(x => x.PlaylistID == playlistId && x.TrackID == trackId).FirstOrDefault();
            context.Remove(itemToDelete);
            context.SaveChanges();
        }

        public ICollection<Playlist> GetAllPlaylists()
        {
            return context.Playlists.ToList();
        }

        public Playlist GetPlaylistById(int id)
        {
            return context.Playlists.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool PlaylistExists(int id)
        {
            return context.Playlists.Any(x => x.Id == id);
        }

        public ICollection<Track> GetTracksFromPlaylist(int playlistId)
        {
            return context.PlaylistTracks.Where(x => x.PlaylistID == playlistId).Select(y => y.Track).ToList();            
        }
    }
}
