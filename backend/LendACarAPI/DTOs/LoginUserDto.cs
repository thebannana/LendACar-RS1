using System.ComponentModel.DataAnnotations;

namespace LendACarAPI.DTOs
{
    public class LoginUserDto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
