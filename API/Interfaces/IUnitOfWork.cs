namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IMessageRepository MessageRepository { get; }
        IGenreRepository GenreRepository { get; }

        Task<bool> Complete();
        bool HasChanges();
    }
}
