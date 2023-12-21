using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public partial class DataContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, GenreName = "Default genre" },
                new Genre { Id = 2, GenreName = "Hip-Hop" },
                new Genre { Id = 3, GenreName = "Soul" },
                new Genre { Id = 4, GenreName = "Jazz" },
                new Genre { Id = 5, GenreName = "Experimental Rock" },
                new Genre { Id = 6, GenreName = "IDM" },
                new Genre { Id = 7, GenreName = "Pop" },
                new Genre { Id = 8, GenreName = "Country" },
                new Genre { Id = 9, GenreName = "Blues" },
                new Genre { Id = 10, GenreName = "Reggae" },
                new Genre { Id = 11, GenreName = "Metal" },
                new Genre { Id = 12, GenreName = "Funk" },
                new Genre { Id = 13, GenreName = "Indie Rock" },
                new Genre { Id = 14, GenreName = "Techno" },
                new Genre { Id = 15, GenreName = "Gospel" }
            );


            modelBuilder.Entity<Artist>().HasData(
                new Artist { Id = 1, ArtistName = "Conway the Machine", ArtistDescription = "Buffalo-based rapper known for his gritty authenticity, raw lyricism, and involvement in the Griselda Records collective. Conway navigates tales of street life and personal struggles, carving a niche in contemporary hip-hop.", GenreId = 2 },                             
                new Artist { Id = 4, ArtistName = "Black Midi", ArtistDescription = "British experimental rock band challenging conventional notions of genre and structure. Characterized by intricate rhythms, dissonant melodies, and unpredictable song structures, Black Midi pushes the boundaries of rock music.", GenreId = 5 },
                new Artist { Id = 5, ArtistName = "Aphex Twin", ArtistDescription = "Electronic musician Richard D. James, known as Aphex Twin, is a pioneer in ambient and electronic music. His innovative sound design and intricate compositions have played a pivotal role in shaping the landscape of electronic music.", GenreId = 6 }
            );


            modelBuilder.Entity<Album>().HasData(
                new Album { Id = 1, AlbumName = "Won't He Do It", Year = "2023", TotalLength = 49.19, ArtistId = 1 },
                new Album { Id = 4, AlbumName = "Hellfire", Year = "2022", TotalLength = 38.54, ArtistId = 4 },
                new Album { Id = 5, AlbumName = "Syro", Year = "2014", TotalLength = 64.31, ArtistId = 5 }
            );

            modelBuilder.Entity<Track>().HasData(
                new Track { Id = 1, TrackName = "Quarters", TrackLength = 2.28, AlbumId = 1 },
                new Track { Id = 2, TrackName = "Monogram", TrackLength = 2.25, AlbumId = 1 },
                new Track { Id = 7, TrackName = "Hellfire", TrackLength = 1.24, AlbumId = 4 },
                new Track { Id = 8, TrackName = "Still", TrackLength = 5.46, AlbumId = 4 },
                new Track { Id = 9, TrackName = "180db_", TrackLength = 3.11, AlbumId = 5 },
                new Track { Id = 10, TrackName = "produk 29", TrackLength = 5.03, AlbumId = 5 }
            );
        }
    }
}
