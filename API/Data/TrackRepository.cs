using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class TrackRepository : ITrackRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TrackRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TrackDto> GetTrackByIdAsync(int id)
        {
            var track = await _context.Tracks.FindAsync(id);

            return _mapper.Map<TrackDto>(track);
        }

        public Track GetTrackByName(string name)
        {
            return _context.Tracks.Where(x => x.TrackName == name).FirstOrDefault();
        }

        public async Task<TrackDto> GetTrackByNameAsync(string name)
        {
            var track = await _context.Tracks
                .Include(x=>x.Genres)
                    .ThenInclude(x=>x.Genre)
                .Include(x=>x.Artists)
                    .ThenInclude(x=>x.Artist)
                .SingleOrDefaultAsync(x => x.TrackName == name);

            return _mapper.Map<TrackDto>(track);
        }

        public async Task<IEnumerable<TrackDto>> GetTracksAsync()
        {
            var result = await _context.Tracks
                .Include(x => x.Artists)
                    .ThenInclude(x => x.Artist)
                .Include(x => x.Genres)
                    .ThenInclude(x => x.Genre)
                .ToListAsync();

            return  _mapper.Map<IEnumerable<TrackDto>>(result);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddNewTrack(Track track, int artistId, int genreId)
        {
            var artistEntity = _context.Artists.Where(x => x.Id == artistId).FirstOrDefault();
            var genres = _context.Genres.Where(x => x.Id == genreId).FirstOrDefault();

            var trackArtist = new TrackArtist() { Artist = artistEntity, Track = track };
            _context.Add(trackArtist);

            var trackGenre = new TrackGenre() { Genre = genres, Track = track };
            _context.Add(trackGenre);

            _context.Add(track);

            return await SaveAllAsync();
        }

        //public void Update(Track track)
        //{
        //    _context.Entry(track).State = EntityState.Modified;
        //}

        public ICollection<Track> GetTracks()
        {            
            return _context.Tracks
                .Include(x => x.Artists)
                    .ThenInclude(x => x.Artist)
                .Include(y => y.Genres)
                    .ThenInclude(y => y.Genre)
                .OrderBy(z => z.Id).ToList();            
        }

        public void UpdateTrack(Track track, int artistId, int genreId)
        {
            _context.Update(track);
            _context.SaveChanges();
        }

        public bool TrackExists(int id)
        {
            return _context.Tracks.Any(x => x.Id == id);
        }

        public void DeleteTrack(Track track)
        {
            _context.Remove(track);
            _context.SaveChanges();
        }

        public Track GetTrackById(int id)
        {
            //var track = await _context.Tracks.FindAsync(id);
            //return _mapper.Map<TrackDto>(track);
            //return _context.Tracks.Find(id);
            return _context.Tracks.Find(id);
        }

        public ICollection<Track> GetTrackByArtist(int artistId)
        {
            return _context.TrackArtists.Where(x => x.ArtistID == artistId).Select(y => y.Track).ToList();
        }

        public ICollection<Track> GetTrackByGenre(int genreId)
        {
            return _context.TrackGenres.Where(x => x.GenreID == genreId).Select(y => y.Track).ToList();
        }
    }
}
