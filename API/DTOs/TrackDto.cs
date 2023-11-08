namespace API.DTOs
{
    public class TrackDto
    {
        public int Id { get; set; }
        public string TrackName { get; set; }
        public double TrackLength { get; set; }        
        public string AlbumName { get; set; }
        public string ArtistName { get; set;}
    }
}
