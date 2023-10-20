using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class RegisterDto
    {
        [Required] public string Username { get; set; }
        [Required] public string KnownAs { get; set; }
        [Required] public DateTime DateOfBirth { get; set; }
        [Required] public string Country { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 4)]
        public string Password { get; set; }
    }
}
