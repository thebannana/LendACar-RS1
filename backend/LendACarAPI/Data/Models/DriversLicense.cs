using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LendACarAPI.Data.Models
{
    public class DriversLicense
    {
        public int Id { get; set; }

        [ForeignKey(nameof(UserId))]
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        public required string LicenseNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime IssuedDate { get; set; }
    }
}
