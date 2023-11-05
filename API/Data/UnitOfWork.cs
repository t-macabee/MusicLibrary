using API.Interfaces;
using API.Services;
using AutoMapper;

namespace API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext context;
        private IMapper mapper;

        public UnitOfWork(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IUserRepository UserRepository => new UserRepository(context, mapper);
        public IMessageRepository MessageRepository => new MessageRepository(context, mapper);
        public IGenreRepository GenreRepository => new GenreRepository(context, mapper);
        public IArtistRepository ArtistRepository => new ArtistRepository(context, mapper);

        public async Task<bool> Complete()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return context.ChangeTracker.HasChanges();
        }
    }
}
