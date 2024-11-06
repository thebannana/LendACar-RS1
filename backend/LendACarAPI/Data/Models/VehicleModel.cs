using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LendACarAPI.Data.Models
{
    public class VehicleModel
    {
        [Key]
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string BrandName { get; set; }

        [ForeignKey(nameof(VehicleCategory))]
        [Required]
        public int VehicleCategoryID { get; set; }
        public VehicleCategory? VehicleCategory { get; set; }
    }
}
