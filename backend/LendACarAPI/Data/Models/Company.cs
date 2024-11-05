using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LendACarAPI.Data.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
        public string CompanyPhone { get; set;}
        public string CompanyEmail { get; set;}

        [ForeignKey(nameof(CompanyEmployee))]
        [Required]
        public int CompanyEmployeeId { get; set; }
        public CompanyEmployee? CompanyEmployee { get; set; }
    }
}
