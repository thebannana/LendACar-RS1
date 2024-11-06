using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LendACarAPI.Data.Models
{
    public class WorkingHour
    {
        public int Id { get; set; }
        public string Days { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        [ForeignKey(nameof(CompanyEmployee))]
        public int CompanyEmployeeId { get; set; }
        public CompanyEmployee? CompanyEmployee { get; set; }
    }
}
