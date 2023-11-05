namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IMessageRepository MessageRepository { get; }
        IGenreRepository GenreRepository { get; }
        IArtistRepository ArtistRepository { get; }

        Task<bool> Complete();
        bool HasChanges();
    }
}
