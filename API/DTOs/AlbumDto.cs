using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;
using API.Entities;

namespace API.DTOs
{
    public class AlbumDto
    {        
        public int Id { get; set; }
        public string AlbumName { get; set; }
        public int AlbumLength { get; set; }
        public DateTime Created { get; set; }
    }
}
