using Azure.Core.GeoJson;
using LendACarAPI.Data;
using LendACarAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LendACarAPI.Endpoints
{
    [ApiController]
    [Route("api/[controller]")]

    public class CompanyEmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CompanyEmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("get/{compEmpId}")]
        public async Task<ActionResult<CompanyEmployee>> GetCompanyEmployeeById(int compEmpId)
        {
            var companyEmployees = await _context.CompanyEmployees
                .Include(ce => ce.User)              // Include User
                .Include(ce => ce.CompanyPosition)   // Include CompanyPosition
                .Include(ce => ce.Company)           // Include Company
                .Include(ce => ce.WorkingHour)       // Include WorkingHour
                .FirstOrDefaultAsync(e => e.UserId == compEmpId);

            if (companyEmployees == null)
            {
                return NotFound("No company employees found");
            }

            return companyEmployees;
        }

        [HttpGet("get/all")]
        public async Task<ActionResult<IEnumerable<CompanyEmployee>>> GetAllEmployeesForAdmin()
        {
            // Get the current user's email from the request headers
            var currentUserEmail = Request.Headers["EmailAddress"].ToString();

            if (string.IsNullOrEmpty(currentUserEmail))
            {
                return Unauthorized("User is not authenticated.");
            }

            // Step 1: Find the company admin using their email
            var companyAdmin = await _context.CompanyEmployees
                .Include(ce => ce.User)  // Include User
                .FirstOrDefaultAsync(ce => ce.CompanyAdminEmail == currentUserEmail);

            if (companyAdmin == null)
            {
                return NotFound("Company admin not found.");
            }

            // Step 2: Get the company ID from the admin
            var companyId = companyAdmin.CompanyId;

            // Step 3: Get all employees of the same company except the one with the company admin email
            var companyEmployees = await _context.CompanyEmployees
                .Include(ce => ce.User)              // Include User
                .Include(ce => ce.CompanyPosition)   // Include CompanyPosition
                .Include(ce => ce.Company)           // Include Company
                .Include(ce => ce.WorkingHour)       // Include WorkingHour
                .Where(ce => ce.CompanyId == companyId && ce.CompanyAdminEmail != currentUserEmail) // Exclude the company admin
                .ToListAsync();

            if (companyEmployees == null || !companyEmployees.Any())
            {
                return NotFound("No employees found for this company.");
            }

            return Ok(companyEmployees);
        }



        [HttpPost("add")]
        public async Task<ActionResult> AddEmployee([FromBody] AddEmployeeRequest request)
        {
            // Validate the request data
            if (request == null)
            {
                return BadRequest(new { message = "Invalid employee data." });
            }

            // Step 1: Find the company admin using their email (this email should exist)
            var companyAdmin = await _context.CompanyEmployees
                .Include(ce => ce.User)
                .FirstOrDefaultAsync(ce => ce.CompanyAdminEmail == request.CompanyAdminEmail);

            if (companyAdmin == null)
            {
                return NotFound(new { message = "Company admin not found." });
            }

            // Step 2: Get the company ID from the admin
            var companyId = companyAdmin.CompanyId;

            // Step 3: Create a new user (from Person table)
            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailAdress = request.Email,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = new byte[0],  // Placeholder for actual password handling
                PasswordSalt = new byte[0],  // Placeholder for actual password handling
                Username = request.FirstName + request.LastName,
                CityId = 1  // Default CityId (adjust as needed)
            };

            // Step 4: Add the new user to the database
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Step 5: Check if the CompanyPosition already exists
            var companyPosition = await _context.CompanyPositions
                .FirstOrDefaultAsync(p => p.Name == request.Title);

            if (companyPosition == null)
            {
                // Create the CompanyPosition if it doesn't exist
                companyPosition = new CompanyPosition
                {
                    Name = request.Title
                };
                _context.CompanyPositions.Add(companyPosition);
                await _context.SaveChangesAsync();
            }

            // Step 6: Create a unique WorkingHour entry for the employee
            var newWorkingHour = new WorkingHour
            {
                StartTime = new TimeOnly(9, 0),  // Example start time
                EndTime = new TimeOnly(17, 0),  // Example end time
                Monday = true,
                Tuesday = true,
                Wednesday = true,
                Thursday = true,
                Friday = true,
                Saturday = false,
                Sunday = false
            };

            _context.WorkingHours.Add(newWorkingHour);
            await _context.SaveChangesAsync();

            // Step 7: Create the CompanyEmployee entry for the new employee
            var companyEmployee = new CompanyEmployee
            {
                UserId = newUser.Id,
                CompanyPositionId = companyPosition.Id,  // Use the existing or newly created position ID
                CompanyId = companyId,
                CompanyAdminEmail = null,  // Only the admin will have this set
                WorkingHourId = newWorkingHour.Id  // Assign the newly created WorkingHourId
            };

            _context.CompanyEmployees.Add(companyEmployee);
            await _context.SaveChangesAsync();

            // Return a JSON response with a message
            return Ok(new { message = "Employee added successfully." });
        }



        [HttpGet("admin-email/{userId}")]
        public IActionResult GetCompanyAdminEmail(int userId)
        {
            var companyEmployee = _context.CompanyEmployees
                                          .FirstOrDefault(e => e.UserId == userId && e.CompanyAdminEmail != null);

            if (companyEmployee != null)
            {
                return Ok(new { companyAdminEmail = companyEmployee.CompanyAdminEmail });
            }
            else
            {
                return NotFound(new { error = "Company Admin Email hasn't been found for this user!" });
            }
        }




    }
}