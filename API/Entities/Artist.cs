namespace API.Entities
{
    public class Artist
    {
        public int Id { get; set; } 
        public string ArtistName { get; set; }
        public string ArtistDescription { get; set;}

        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public ICollection<Album> Albums { get; set; }
        public ICollection<ArtistPhoto> Photos { get; set; }
    }
}
