using API.Entities;

namespace API.DTOs
{
    public class ArtistDto
    {
        public int Id { get; set; }
        public string ArtistName { get; set; }
        public string ArtistDescription { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<Album> Albums { get; set; }
        public ICollection<ArtistPhoto> ArtistPhotos { get; set; }
    }
}
