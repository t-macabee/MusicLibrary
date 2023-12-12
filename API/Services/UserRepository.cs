using API.Data;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class UserRepository : IUserRepository
    {
        private DataContext context;
        private IMapper mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }        

        public async Task<AppUser> GetUserById(int id)
        {
            return await context.Users
                .Include(x => x.Playlists)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<AppUser> GetUserByUsername(string username)
        {
            return await context.Users
                .Include(x => x.Photos)
                .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsers()
        {
            return await context.Users
                .Include(x => x.Photos)
                .ToListAsync();
        }
       
        public async Task<PagedList<MemberDto>> GetMembers(UserParams userParams)
        {
            var query = context.Users.AsQueryable();
                
            query = query.Where(x => x.UserName != userParams.CurrentUsername);

            query = query.OrderByDescending(x => x.Created);

            return await PagedList<MemberDto>
                .CreateAsync(query.ProjectTo<MemberDto>(mapper.ConfigurationProvider)
                .AsNoTracking(), 
                userParams.PageNumber,
                userParams.PageSize);
        }

        public async Task<MemberDto> GetMember(string username)
        {
            return await context.Users
                .Where(x => x.UserName == username)
                .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public void Update(AppUser user)
        {
            context.Entry(user).State = EntityState.Modified;
        }
    }
}
