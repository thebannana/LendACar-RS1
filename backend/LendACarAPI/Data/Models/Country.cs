using System.ComponentModel.DataAnnotations;

namespace LendACarAPI.Data.Models;

public class Country
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }

    // public List<City> Cities{ get; set; }

}