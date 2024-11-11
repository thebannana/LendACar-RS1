using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LendACarAPI.Data.Models
{
    public class User:Person
    {

        [Required]
        [EmailAddress]
        public string EmailAdress { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }
        public DateTime CreatedDate { get; set; }
        public double AverageRating { get; set; } = 0.0;

        public DriversLicense DriversLicense { get; set; }

    }
}
