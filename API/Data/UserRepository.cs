using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .FindAsync(id);
        }

        public AppUser GetUserById(int id)
        {
            return _context.Users.Where(x => x.Id == id).Include(y => y.Photos).FirstOrDefault();
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(x=>x.Photos)
                .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<AppUserDto> GetUserDtoAsync(string username)
        {
            return await _context.Users
                .Where(x=>x.UserName == username)
                .ProjectTo<AppUserDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }
        public async Task<PagedList<AppUserDto>> GetUsersDtoAsync(UserParams userParams)
        {
            var query = _context.Users
                .ProjectTo<AppUserDto>(_mapper.ConfigurationProvider)
                .AsNoTracking();

            return await PagedList<AppUserDto>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users
                .Include(x=>x.Photos)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}
