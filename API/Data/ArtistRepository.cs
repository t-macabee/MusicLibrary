using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ArtistRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddNewArtist(ArtistDto artist)
        {
            var temp = await _context.AddAsync(_mapper.Map<Artist>(artist));
            return await SaveAllAsync();
        }

        public async Task<ArtistDto> GetArtistByIdAsync(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            return _mapper.Map<ArtistDto>(artist);
        }

        public async Task<ArtistDto> GetArtistByNameAsync(string name)
        {
            var artist = await _context.Artists.SingleOrDefaultAsync(x => x.ArtistName == name);
            return _mapper.Map<ArtistDto>(artist);
        }

        public async Task<IEnumerable<ArtistDto>> GetArtistsAsync()
        {
            var artist = await _context.Artists.ToListAsync();
            return _mapper.Map<IEnumerable<ArtistDto>>(artist);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateArtist(Artist artist)
        {
            _context.Update(artist);
            _context.SaveChanges();
        }

        public void DeleteArtist(Artist artist)
        {
            _context.Remove(artist);
            _context.SaveChanges();
        }

        public bool ArtistExists(int id)
        {
            return _context.Artists.Any(x => x.Id == id);
        }

        public Artist GetArtistById(int id)
        {
            return _context.Artists.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
