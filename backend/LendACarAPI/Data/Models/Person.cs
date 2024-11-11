using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LendACarAPI.Data.Models
{
    public abstract class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        [Phone]
        public string? PhoneNumber { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }

        [ForeignKey(nameof(Country))]
        [Required]
        public int CountryId { get; set; }
        public Country? Country { get; set; }

    }
}
