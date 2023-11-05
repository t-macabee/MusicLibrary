namespace API.Entities
{
    public class Artist
    {
        public int Id { get; set; } 
        public string ArtistName { get; set; }
        public string ArtistDescription { get; set;}

        public ICollection<Album> Albums { get; set; }
        public ICollection<ArtistPhoto> ArtistPhotos { get; set; }
    }
}
