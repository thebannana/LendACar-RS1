using System.ComponentModel.DataAnnotations;

namespace LendACarAPI.Data.Models
{
    public class CompanyPosition
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
