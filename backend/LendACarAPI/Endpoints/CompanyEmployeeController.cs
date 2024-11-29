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

        // POST method to add a new employee
        [HttpPost("add")]
        public async Task<ActionResult> AddEmployee([FromBody] AddEmployeeRequest request)
        {
            // Validate the request data
            if (request == null)
            {
                return BadRequest("Invalid employee data.");
            }

            // Step 1: Find the company admin using their email (this email should exist)
            var companyAdmin = await _context.CompanyEmployees
                .Include(ce => ce.User)
                .FirstOrDefaultAsync(ce => ce.CompanyAdminEmail == request.CompanyAdminEmail);

            if (companyAdmin == null)
            {
                return NotFound("Company admin not found.");
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
                // For now we are setting empty password fields (should be handled properly)
                PasswordHash = new byte[0],  // You should generate this based on actual user input
                PasswordSalt = new byte[0],  // You should generate this based on actual user input
                Username = request.FirstName + request.LastName,
                // Set default CityId for now (assuming 1 is a valid CityId in your database)
                CityId = 1  // Default CityId
            };

            // Step 4: Add the new user to the database
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Step 5: Create the CompanyPosition for the employee
            var newPosition = new CompanyPosition
            {
                Name = request.Title,  // Set the title of the employee (the position)
            };

            _context.CompanyPositions.Add(newPosition);
            await _context.SaveChangesAsync();

            // Step 6: Check if there is a default WorkingHours record, or create one if necessary
            var defaultWorkingHour = await _context.WorkingHours
                .FirstOrDefaultAsync(w => w.StartTime == new TimeOnly(9, 0) && w.EndTime == new TimeOnly(17, 0)); // assuming the client passes this ID

            if (defaultWorkingHour == null)
            {
                // Create a default working hours record if one doesn't exist
                defaultWorkingHour = new WorkingHour
                {
                    StartTime = new TimeOnly(9,0),  // Example start time
                    EndTime = new TimeOnly(17,0),  // Example end time
                    Monday = true,
                    Tuesday = true,
                    Wednesday = true,
                    Thursday = true,
                    Friday = true,
                    Saturday = false,  // Assuming weekends off
                    Sunday = false     // Assuming weekends off
                };

                _context.WorkingHours.Add(defaultWorkingHour);
                await _context.SaveChangesAsync();
            }

            // Step 7: Create the CompanyEmployee entry for the new employee
            var companyEmployee = new CompanyEmployee
            {
                UserId = newUser.Id,
                CompanyPositionId = newPosition.Id,
                CompanyId = companyId,  // The same company as the admin
                CompanyAdminEmail = null,  // Only the admin will have this field set
                WorkingHourId = defaultWorkingHour.Id  // Assign the default WorkingHourId
            };

            _context.CompanyEmployees.Add(companyEmployee);
            await _context.SaveChangesAsync();

            return Ok("Employee added successfully.");
        }





    }
}