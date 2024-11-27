using System.ComponentModel.DataAnnotations;

namespace LendACarAPI.DTOs
{
    public class AdministratorLoginDto
    {
        [Required]
        public string? EmailAddress { get; set; }
        [Required]
        public string? Password { get; set; }


    }
}
