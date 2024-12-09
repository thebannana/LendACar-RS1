using System.ComponentModel.DataAnnotations;

namespace LendACarAPI.Data.Models
{
    public class VehicleCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        // Obrisati Description s obzirom da se userima nigdje nece prikazivati (bar nema razloga da se prikazuje)
        public string? Description { get; set; }

        // Koristiti cemo string sa URL-om na ikonice kategorija vozila
        //[Required]
        //public byte[] VehicleCategoryIcon { get; set; }

    }
}
