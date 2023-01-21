using API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace API.Data
{
    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<AlbumTrack> AlbumTracks { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<ExceptionHandlingData> ExceptionData { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<PhotoAlbum> PhotoAlbums { get; set; }
        public DbSet<PhotoUser> PhotoUsers { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistTrack> PlaylistTracks { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<TrackArtist> TrackArtists { get; set; }
        public DbSet<TrackGenre> TrackGenres { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrackArtist>()
                .HasKey(k => new { k.TrackID, k.ArtistID });
            modelBuilder.Entity<TrackGenre>()
                .HasKey(k => new { k.GenreID, k.TrackID });
            modelBuilder.Entity<PlaylistTrack>()
                .HasKey(k => new { k.PlaylistID, k.TrackID });
            modelBuilder.Entity<AlbumTrack>()
                .HasKey(k => new { k.AlbumID, k.TrackID });          

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
