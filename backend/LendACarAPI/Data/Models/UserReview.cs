using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LendACarAPI.Data.Models
{
    public class UserReview
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey(nameof(Reviewer))]
        [Required]
        public int ReviewerId { get; set; }
        public User? Reviewer { get; set; }

        public int Rating { get; set; }
        public string Description { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
