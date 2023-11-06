using API.Entities;

namespace API.Interfaces
{
    public interface ITrackRepository
    {
        void CreateTrack(Track track);
        void UpdateTrack(Track track);
        void DeleteTrack(Track track);

        Task<IEnumerable<Track>> GetAllTracks();
        Task<Track> GetTrackByIdAsync(int id);
        Task<Track> GetTrackByNameAsync(string name);
    }
}
