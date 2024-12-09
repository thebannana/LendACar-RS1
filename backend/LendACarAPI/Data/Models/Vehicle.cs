using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LendACarAPI.Data.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        public float? EngineDisplacement { get; set; }
        public int? EnginePower { get; set; }
        [Required]
        public string TransmissionType { get; set; }
        [Required]
        public string FuelType { get; set; }
        [Required]
        public string AirConditioning { get; set; }
        public int NumberOfSeats { get; set; }
        public bool TowingHitch { get; set; }
        public int? MaximumLoad { get; set; }
        public string Description { get; set; }
        [Required]
        public bool Avaliable { get; set; } = false;
        public double AverageRating { get; set; } = 0.0;
        [Required]
        public double PricePerDay { get; set; }


        [ForeignKey(nameof(VehicleModel))]
        [Required]
        public int VehicleModelID { get; set; }
        public VehicleModel? VehicleModel { get; set; }

        [ForeignKey(nameof(VehicleCategory))]
        [Required]
        public int VehicleCategoryID { get; set; }
        public VehicleCategory? VehicleCategory { get; set; }


        [ForeignKey(nameof(VehicleOwner))]
        [Required]
        public int VehicleOwnerID { get; set; }
        public User? VehicleOwner { get; set; }

    }
}
