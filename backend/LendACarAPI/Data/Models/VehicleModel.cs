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
        [Required]
        public string BrandName { get; set; }

        [ForeignKey(nameof(VehicleCategory))]
        [Required]
        public int VehicleCategoryID { get; set; }
        public VehicleCategory? VehicleCategory { get; set; }
    }
}
