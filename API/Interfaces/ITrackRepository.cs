using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface ITrackRepository
    {

        Task<bool> SaveAllAsync();
        Task<IEnumerable<TrackDto>> GetTracksAsync();
        Task<TrackDto> GetTrackByIdAsync(int id);
        Task<TrackDto> GetTrackByNameAsync(string name);       

        //-------------------------------------------------------------
        Task<bool> AddNewTrack(Track track, int artistId, int genreId);
        void UpdateTrack(Track track, int artistId, int genreId);
        void DeleteTrack(Track track);
        ICollection<Track> GetTracks();
        ICollection<Track> GetTrackByArtist(int artistId);
        ICollection<Track> GetTrackByGenre(int genreId);
        Track GetTrackById(int id);
        bool TrackExists(int id);

    }
}
