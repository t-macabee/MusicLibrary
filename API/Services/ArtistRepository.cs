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

        public ArtistRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
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

        public async Task<Artist> GetArtistById(int id)
        {
            return await context.Artists
                .Include(x => x.Photos)
                .Include(x => x.Genre)
                .Include(x => x.Albums)
                    .ThenInclude(a => a.Tracks) 
                .SingleOrDefaultAsync(x => x.Id == id);
        }        

        public async Task<IEnumerable<Artist>> GetAllArtists()
        {
            return await context.Artists
                .Include(x => x.Photos)
                .Include(x => x.Albums)
                .ThenInclude(y => y.Tracks)
                .Include(x => x.Genre)
                .ToListAsync();
        }        
    }
}
