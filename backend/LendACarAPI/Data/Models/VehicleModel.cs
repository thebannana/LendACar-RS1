using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LendACarAPI.Data.Models
{
    public class VehicleModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ModelName { get; set; }

        [ForeignKey(nameof(VehicleBrand))]
        [Required]
        public int VehicleBrandId { get; set; }
        public VehicleBrand? VehicleBrand { get; set; }

    }
}
