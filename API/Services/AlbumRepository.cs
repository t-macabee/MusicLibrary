using API.Data;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class AlbumRepository : IAlbumRepository
    {
        private DataContext context;
        private IMapper mapper;

        public AlbumRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void CreateAlbum(Album album)
        {
            context.Albums.Add(album);
        }

        public void DeleteAlbum(Album album)
        {
            context.Albums.Remove(album);
        }

        public void UpdateAlbum(Album album)
        {
            context.Entry(album).State = EntityState.Modified;
        }

        public async Task<IEnumerable<Album>> GetAllAlbums()
        {
            return await context.Albums                  
              .Include(x => x.Tracks)
              .ToListAsync();
        }

        public async Task<Album> GetAlbumByIdAsync(int albumId)
        {
            return await context.Albums.FindAsync(albumId);
        }

        public async Task<Album> GetAlbumByNameAsync(string albumName)
        {
            return await context.Albums
                .Include(x => x.Tracks)
                .SingleOrDefaultAsync(x => x.AlbumName.ToLower() == albumName.ToLower());
        }       
    }
}
