using API.Entities;
using API.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace API.Data
{
    public class GenreRepository : IGenreRepository
    {
        private DataContext context;
        public GenreRepository(DataContext context)
        {
            this.context = context;        
        }

        public void CreateGenre(Genre genre)
        {
            context.Add(genre);
            context.SaveChanges();
        }

        public void UpdateGenre(Genre genre)
        {
            context.Update(genre);
            context.SaveChanges();
        }

        public void DeleteGenre(Genre genre)
        {
            context.Remove(genre);
            context.SaveChanges();
        }

        public bool GenreExists(int id)
        {
            return context.Genres.Any(x => x.Id == id);
        }
        

        public Genre GetGenreById(int id)
        {
            return context.Genres.Where(x => x.Id == id).FirstOrDefault();
        }

        public Genre GetGenreByName(string name)
        {
            return context.Genres.Where(x => x.GenreName == name).FirstOrDefault();
        }

        public ICollection<Genre> GetGenres()
        {
            return context.Genres.OrderBy(x => x.Id).ToList();
        }

    }
}
