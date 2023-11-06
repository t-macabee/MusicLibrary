﻿namespace API.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
        public ICollection<Artist> Artists { get; set; }
    }
}
