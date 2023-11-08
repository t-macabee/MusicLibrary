using System.Text.Json.Serialization;

namespace API.Entities
{
    public class Playlist
    {
        public int Id { get; set; }       
        public string PlaylistName { get; set; }
        public string PlaylistDescription { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public ICollection<PlaylistTrack> PlaylistTracks { get; set; }
    }
}
