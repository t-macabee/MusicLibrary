namespace API.DTOs
{
    public class AlbumDto
    {
        public int Id { get; set; }
        public string AlbumName { get; set; }
        public string Year { get; set; }
        public double TotalLength { get; set; }
        public ICollection<TrackDto> Tracks { get; set; }
    }
}
