using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LendACarAPI.Data.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string CompanyDescription { get; set; }
        [Required]
        [Phone]
        public string CompanyPhone { get; set;}
        [EmailAddress]
        public string? CompanyEmail { get; set;}
        [Required]
        public byte[] CompanyAvatar { get; set; }

        public double AverageRating { get; set; } = 0.0;

    }
}
