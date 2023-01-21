using API.Entities;
using System.Collections.Generic;

namespace API.Interfaces
{
    public interface IGenreRepository
    {
        ICollection<Genre> GetGenres();
        //ICollection<Artist> GetArtistByGenre();
        Genre GetGenreById(int id);
        Genre GetGenreByName(string name);
        bool GenreExists(int id);
        void CreateGenre(Genre genre);
        void UpdateGenre(Genre genre);
        void DeleteGenre(Genre genre);       
    }
}
