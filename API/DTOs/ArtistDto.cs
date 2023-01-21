using System.ComponentModel.DataAnnotations;
using System;

namespace API.DTOs
{
    public class ArtistDto
    {
        public int Id { get; set; }
        public string ArtistName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string About { get; set; }        
    }
}
