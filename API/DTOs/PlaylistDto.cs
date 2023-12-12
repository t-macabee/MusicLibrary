namespace API.DTOs
{
    public class PlaylistDto
    {
        public int Id { get; set; }
        public string PlaylistName { get; set; }
        public IEnumerable<TrackDto> Tracks { get; set; }
    }
}
