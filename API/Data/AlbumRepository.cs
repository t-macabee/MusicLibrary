using API.Entities;
using API.Interfaces;
using AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace API.Data
{
    public class AlbumRepository : IAlbumRepository
    {
        public DataContext context;

        public AlbumRepository(DataContext context)
        {
            this.context = context;
        }

        public bool AlbumExists(int id)
        {
            return context.Albums.Any(x => x.Id == id);
        }

        public void CreateAlbum(Album album, int trackId)
        {
            var trackEntity = context.Tracks.Where(x => x.Id == trackId).FirstOrDefault();

            var albumTracks = new AlbumTrack()
            {
                Album = album,
                Track = trackEntity
            };

            context.Add(albumTracks);
            context.Add(album);
            context.SaveChanges();
        }

        public void UpdateAlbum(Album album)
        {
            context.Update(album);
            context.SaveChanges();
        }

        public void DeleteAlbum(Album album)
        {
            context.Remove(album);
            context.SaveChanges();
        }

        public Album GetAlbumById(int id)
        {
            return context.Albums.Where(x => x.Id == id).FirstOrDefault();
        }

        public Album GetAlbumByName(string name)
        {
            return context.Albums.Where(x => x.AlbumName == name).FirstOrDefault();
        }

        public ICollection<Album> GetAllAlbums()
        {
            return context.Albums.OrderBy(x => x.Id).ToList();
        }

        public void AddTrackToAlbum(int albumId, int trackId)
        {
            var existingTrack = context.Tracks.Where(x => x.Id == trackId).FirstOrDefault();
            var existingAlbum = context.Albums.Where(x => x.Id == albumId).FirstOrDefault();

            var albumTrack = new AlbumTrack()
            {
                Album = existingAlbum,
                Track = existingTrack
            };

            context.Add(albumTrack);
            context.SaveChanges();
        }

        public ICollection<Track> GetAllTracksByAlbum(int albumId)
        {
            return context.AlbumTracks.Where(x => x.AlbumID == albumId).Select(y => y.Track).ToList();
        }
    }
}
