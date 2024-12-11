using LendACarAPI.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LendACarAPI.DTOs
{
    public class VerificationDto
    {
        public int Id { get; set; }
        [Required]
        public DateTime RequestDate { get; set; }

        [Required]
        public int UserId { get; set; }

        public string? RequestComment { get; set; } // mogucnost useru da ostavi komentar koji ce biti vidljiv uposleniku

        public int EmployeeId { get; set; }

    }
}
