using API.Data;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private DataContext context;
        private IMapper mapper;

        public PlaylistRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void CreatePlaylist(Playlist playlist)
        {
            context.Playlists.Add(playlist);
        }

        public void DeletePlaylist(Playlist playlist)
        {
            context.Playlists.Remove(playlist);
        }

        public void UpdatePlaylist(Playlist playlist)
        {
            context.Entry(playlist).State = EntityState.Modified;
        }

        public async Task<IEnumerable<Playlist>> GetAllPlaylists()
        {
            return await context.Playlists
                .Include(x => x.PlaylistTracks)
                .ThenInclude(y => y.Track)
                .ToListAsync();
        }

        public async Task<IEnumerable<Playlist>> GetAllPlaylistsByUserAsync(int userId)
        {
            return await context.Playlists
                .Where(x => x.AppUserId == userId)
                .Include(x => x.PlaylistTracks)
                .ThenInclude(y => y.Track)
                .ToListAsync();
        }

        public async Task<Playlist> GetPlaylistByIdAsync(int id)
        {
            return await context.Playlists
                .Include(x => x.PlaylistTracks)
                .ThenInclude(y => y.Track)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Playlist> GetPlaylistByNameAsync(string name)
        {
            return await context.Playlists
                .Include(x => x.PlaylistTracks)
                .ThenInclude(y => y.Track)
                .SingleOrDefaultAsync(x => x.PlaylistName.ToLower() == name.ToLower());
        }

        public async Task AddTrackToPlaylist(int playlistId, int trackId)
        {
            var playlistTrack = new PlaylistTrack
            {
                PlaylistId = playlistId,
                TrackId = trackId
            };            
            context.PlaylistTracks.Add(playlistTrack);
        }

        public async Task RemoveTrackFromPlaylist(int playlistId, int trackId)
        {
            var playlistTrack = await context.PlaylistTracks.FirstOrDefaultAsync(pt => pt.PlaylistId == playlistId && pt.TrackId == trackId);

            if (playlistTrack != null)
            {                
                context.PlaylistTracks.Remove(playlistTrack);
            }
        }               
    }
}
