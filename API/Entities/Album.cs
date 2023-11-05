using System.Text.Json.Serialization;

namespace API.Entities
{
    public class Album
    {
        public int Id { get; set; }
        public string AlbumName { get; set; }
        public string Year { get; set; }
        public int ArtistId { get; set; }        
        public Artist Artist { get; set; }
        public ICollection<Track> Tracks { get; set; }
    }
}
