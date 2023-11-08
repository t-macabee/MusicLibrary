namespace API.Entities
{
    public class Track
    {
        public int Id { get; set; }
        public string TrackName { get; set; }
        public double TrackLenght { get; set; }
        
        public int AlbumId { get; set; }
        public Album Album { get; set; }

        public ICollection<PlaylistTrack> PlaylistTracks { get; set; }
    }
}
