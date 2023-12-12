using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<AppUser> GetUserById(int id);
        Task<IEnumerable<AppUser>> GetUsers();
        Task<AppUser> GetUserByUsername(string username);

        Task<PagedList<MemberDto>> GetMembers(UserParams userParams);
        Task<MemberDto> GetMember(string username);       
    }
}
