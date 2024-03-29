﻿namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IMessageRepository MessageRepository { get; }
        IGenreRepository GenreRepository { get; }
        IArtistRepository ArtistRepository { get; }
        IAlbumRepository AlbumRepository { get; }
        ITrackRepository TrackRepository { get; }
        IPlaylistRepository PlaylistRepository { get; }

        Task<bool> Complete();
        bool HasChanges();
    }
}
