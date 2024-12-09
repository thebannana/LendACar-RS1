using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LendACarAPI.Data.Models
{


    public class VerificationRequest
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime RequestDate { get; set; }


        [ForeignKey(nameof(User))]
        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }

        public string? RequestComment { get; set; } // mogucnost useru da ostavi komentar koji ce biti vidljiv uposleniku

        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        [Required]
        public string RequestDenialReason { get; set; }
    }
}
