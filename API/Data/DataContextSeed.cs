using API.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Data
{
    public partial class DataContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData
                (
                    new Genre { Id = 1, GenreName = "Genre 1"},
                    new Genre { Id = 2, GenreName = "Genre 2"},
                    new Genre { Id = 3, GenreName = "Genre 3"},
                    new Genre { Id = 4, GenreName = "Genre 4"},
                    new Genre { Id = 5, GenreName = "Genre 5"},
                    new Genre { Id = 6, GenreName = "Genre 6"},
                    new Genre { Id = 7, GenreName = "Genre 7"}
                );

            modelBuilder.Entity<Artist>().HasData
               (
                   new Artist { Id = 1, ArtistName = "Artist 1", DateOfBirth = DateTime.Now, Gender = "M", About = "Fact 1"},
                   new Artist { Id = 2, ArtistName = "Artist 2", DateOfBirth = DateTime.Now, Gender = "F", About = "Fact 2"},
                   new Artist { Id = 3, ArtistName = "Artist 3", DateOfBirth = DateTime.Now, Gender = "M", About = "Fact 3"},
                   new Artist { Id = 4, ArtistName = "Artist 4", DateOfBirth = DateTime.Now, Gender = "F", About = "Fact 4"},
                   new Artist { Id = 5, ArtistName = "Artist 5", DateOfBirth = DateTime.Now, Gender = "F", About = "Fact 5"},
                   new Artist { Id = 6, ArtistName = "Artist 6", DateOfBirth = DateTime.Now, Gender = "M", About = "Fact 6"},
                   new Artist { Id = 7, ArtistName = "Artist 7", DateOfBirth = DateTime.Now, Gender = "M", About = "Fact 7"}                   
               );            
        }
    }
}
