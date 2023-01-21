using API.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ArtistName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string About { get; set; }        

        //public byte[] Image { get; set; }

        public int GetAge()
        {
            return DateOfBirth.CalculateAge();
        }
    }
}
