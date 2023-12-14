using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface ITrackRepository
    {
        void CreateTrack(Track track);
        void UpdateTrack(Track track);
        void DeleteTrack(Track track);

        Task<IEnumerable<TrackDto>> GetAllTracks();
        Task<IEnumerable<TrackDto>> GetTracksByAlbum(int id);
        Task<Track> GetTrackById(int id);
    }
}
