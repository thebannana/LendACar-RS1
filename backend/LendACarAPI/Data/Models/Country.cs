using System.ComponentModel.DataAnnotations;

namespace LendACarAPI.Data.Models;

public class Country
{
    [Key]
    public int ID { get; set; }
    public string Name { get; set; }

    // public List<City> Cities{ get; set; }

}