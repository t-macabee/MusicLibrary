using API.Entities;

namespace API.DTOs
{
    public class ArtistDto
    {
        public int Id { get; set; }
        public string ArtistName { get; set; }
        public string ArtistDescription { get; set; }
        public int GenreId { get; set; }
        public GenreDto Genre { get; set; }  
        public ICollection<AlbumDto> Albums { get; set; }
    }
}
