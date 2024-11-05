using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LendACarAPI.Data.Models
{
    public class User
    {
        [Key]
        [ForeignKey(nameof(Person))]
        [Required]
        public int PersonId { get; set; }
        public Person? Person { get; set; }
        [Required]
        public string EmailAdress { get; set; }
        [Required]
        public string Username { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
