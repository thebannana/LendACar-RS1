using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LendACarAPI.Data.Models
{
    public class UpdateEmployeeDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Title { get; set; }
        public string CompanyAdminEmail { get; set; } // Used for validation
    }

}