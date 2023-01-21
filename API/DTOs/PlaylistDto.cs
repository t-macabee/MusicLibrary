using API.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace API.DTOs
{
    public class PlaylistDto
    {
        public int Id { get; set; }
        public string PlaylistName { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
