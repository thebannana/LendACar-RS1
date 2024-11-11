using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LendACarAPI.Data.Models
{
    public class CompanyReview
    {
        [ForeignKey(nameof(Reviewer))]
        [Required]
        public int ReviewerId { get; set; }
        public User? Reviewer { get; set; }

        [ForeignKey(nameof(Company))]
        [Required]
        public int CompanyId { get; set; }
        public Company? Company { get; set; }

        public int Rating { get; set; }
        public string? Description { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
