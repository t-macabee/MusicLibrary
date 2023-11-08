using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class ArtistRepository : IArtistRepository
    {
        private DataContext context;
        private IMapper mapper;

        public ArtistRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void CreateArtist(Artist artist)
        {
            context.Artists.Add(artist);
        }

        public void UpdateArtist(Artist artist)
        {
            context.Entry(artist).State = EntityState.Modified;
        }

        public void DeleteArtist(Artist artist)
        {
            context.Artists.Remove(artist);
        }

        public async Task<Artist> GetArtistByIdAsync(int id)
        {
            return await context.Artists
                .Include(x => x.Genre)
                .Include(x => x.Albums)
                    .ThenInclude(a => a.Tracks) 
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Artist> GetArtistByNameAsync(string name)
        {
            return await context.Artists
                .Include(x => x.Genre)
                .Include(x => x.Albums)
                    .ThenInclude(y => y.Tracks)
                .SingleOrDefaultAsync(x => x.ArtistName.ToLower() == name.ToLower());           
        }

        public async Task<IEnumerable<Artist>> GetArtistsByGenre(string name)
        {
            return await context.Artists
                .Include(x => x.Genre) 
                .Include(x => x.Albums)
                .ThenInclude(y => y.Tracks)
                .Where(x => x.Genre.GenreName.ToLower() == name.ToLower())                
                .ToListAsync();
        }

        public async Task<IEnumerable<Artist>> GetAllArtistsAsync()
        {
            return await context.Artists
                .Include(x => x.Albums)
                .ThenInclude(y => y.Tracks)
                .Include(x => x.Genre)
                .ToListAsync();
        }        
    }
}
