using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class Playlist
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PlaylistName { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public ICollection<PlaylistTrack> PlaylistTracks { get; set; }
    }
}
