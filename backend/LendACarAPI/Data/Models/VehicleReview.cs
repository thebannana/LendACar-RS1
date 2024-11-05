using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LendACarAPI.Data.Models
{
    public class VehicleReview
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Reviewer))]
        [Required]
        public int ReviewerId { get; set; }
        public User? Reviewer { get; set; }

        [ForeignKey(nameof(Vehicle))]
        [Required]
        public int VehicleID { get; set; }
        public Vehicle? Vehicle { get; set; }
        [Required]
        public int Rating { get; set; }
        public string Description { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
