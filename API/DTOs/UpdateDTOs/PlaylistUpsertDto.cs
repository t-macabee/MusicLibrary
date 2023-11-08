namespace API.DTOs.UpdateDTOs
{
    public class PlaylistUpsertDto
    {
        public string PlaylistName { get; set; }
        public string PlaylistDescription { get; set; }
        public DateTime? DateModified {  get; set; }
    }
}
