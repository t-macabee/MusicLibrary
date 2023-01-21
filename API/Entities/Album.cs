using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Album
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Album name")]
        [Required]
        public string AlbumName { get; set; }
        public int AlbumLength { get; set; }
        public DateTime Created { get; set; }
        public ICollection<AlbumTrack> AlbumTracks { get; set; }
        public ICollection<PhotoAlbum> AlbumPhotos { get; set; }
    }
}
