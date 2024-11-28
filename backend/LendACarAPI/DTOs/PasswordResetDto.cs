using System.ComponentModel.DataAnnotations;

namespace LendACarAPI.DTOs
{
    public class PasswordResetDto
    {
        [Required]
        public string? emailAddress { get; set; }
        [Required]
        public string? password { get; set; }
    }
}
