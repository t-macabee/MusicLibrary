﻿namespace API.Entities
{
    public class ArtistPhoto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }

        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
    }
}
