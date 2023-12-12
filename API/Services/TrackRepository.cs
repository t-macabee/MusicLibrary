using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class TrackRepository : ITrackRepository
    {
        private DataContext context;
        private IMapper mapper;

        public TrackRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void CreateTrack(Track track)
        {
            context.Tracks.Add(track);
        }

        public void DeleteTrack(Track track)
        {
            context.Tracks.Remove(track);
        }

        public void UpdateTrack(Track track)
        {
            context.Entry(track).State = EntityState.Modified;
        }

        public async Task<IEnumerable<TrackDto>> GetAllTracks()
        {
            return await context.Tracks
                .Include(x => x.Album)
                    .ThenInclude(x => x.Artist)
                .Select(track => new TrackDto
                {
                    Id = track.Id,
                    TrackName = track.TrackName,
                    TrackLength = track.TrackLength,
                    AlbumName = track.Album.AlbumName,
                    ArtistName = track.Album.Artist.ArtistName
                })
                .ToListAsync();
        }

        public async Task<Track> GetTrackById(int id)
        {            
            return await context.Tracks
            .Include(x => x.Album)
                .ThenInclude(x => x.Artist)
            .FirstOrDefaultAsync(a => a.Id == id);            
        }

        public async Task<TrackDto> GetTrackByName(string name)
        {
            return await context.Tracks
                .Include(x => x.Album)
                    .ThenInclude(x => x.Artist)
                .Select(track => new TrackDto
                {
                    Id = track.Id,
                    TrackName = track.TrackName,
                    TrackLength = track.TrackLength,
                    AlbumName = track.Album.AlbumName,
                    ArtistName = track.Album.Artist.ArtistName
                })
                .SingleOrDefaultAsync(x => x.TrackName.ToLower() == name.ToLower());
        }

        public async Task<IEnumerable<TrackDto>> GetTracksByAlbum(int albumId)
        {
            return await context.Tracks
                .Where(x => x.AlbumId == albumId)
                .Include(x => x.Album)
                    .ThenInclude(x => x.Artist)
                .Select(track => new TrackDto
                {
                    Id = track.Id,
                    TrackName = track.TrackName,
                    TrackLength = track.TrackLength,
                    AlbumName = track.Album.AlbumName,
                    ArtistName = track.Album.Artist.ArtistName
                })
                .ToListAsync();
        }
    }
}
