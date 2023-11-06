namespace API.DTOs
{
    public class TrackDto
    {
        public int Id { get; set; }
        public string TrackName { get; set; }
        public string TrackLength { get; set; }
        public int GenreId { get; set; } 
    }
}
