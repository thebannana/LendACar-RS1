using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LendACarAPI.Data.Models
{
    public class CompanyEmployee
    {
        [Key]
        public int Id { get; set; }
        public string CompanyAdminEmail { get; set; }

        [ForeignKey(nameof(User))]
        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey(nameof(CompanyPosition))]
        [Required]
        public int CompanyPositionId { get; set; }
        public CompanyPosition? CompanyPosition { get; set; }
    }
}
