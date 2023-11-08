using API.Data;
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

        public async Task<IEnumerable<Track>> GetAllTracks()
        {
            return await context.Tracks.ToListAsync();
        }

        public async Task<Track> GetTrackByIdAsync(int id)
        {
            return await context.Tracks.FirstOrDefaultAsync(a => a.Id == id); 
        }

        public async Task<Track> GetTrackByNameAsync(string name)
        {
            return await context.Tracks.SingleOrDefaultAsync(x => x.TrackName.ToLower() == name.ToLower());
        }       
    }
}
