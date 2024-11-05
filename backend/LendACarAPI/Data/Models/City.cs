using LendACarAPI.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LendACarAPI.Data.Models;

public class City
{
    [Key]
    public int ID { get; set; }
    [Required]
    public string Name { get; set; }


    [ForeignKey(nameof(Country))]
    [Required]
    public int CountryId { get; set; }
    public Country? Country { get; set; }
}