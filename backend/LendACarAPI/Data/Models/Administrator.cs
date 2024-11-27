using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LendACarAPI.Data.Models
{
    
    
    public class Administrator:Person
    {
        [Required]
        [EmailAddress]
        public string EmailAdress { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }
        [Required] 
        public byte[] PasswordSalt { get; set; }

        [Required]
        public string Username { get; set; }

    }
}
