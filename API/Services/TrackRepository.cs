using API.Data;
using API.Entities;
using API.Interfaces;
using AutoMapper;

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
            throw new NotImplementedException();
        }

        public void DeleteTrack(Track track)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Track>> GetAllTracks()
        {
            throw new NotImplementedException();
        }

        public Task<Track> GetTrackByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Track> GetTrackByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public void UpdateTrack(Track track)
        {
            throw new NotImplementedException();
        }
    }
}
