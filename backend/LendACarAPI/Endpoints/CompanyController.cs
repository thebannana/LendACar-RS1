using LendACarAPI.Data;
using LendACarAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LendACarAPI.Endpoints
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CompanyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // This endpoint checks if the logged-in user has a company
        [HttpGet("check/{userId}")]
        public async Task<ActionResult<bool>> CheckIfCompanyExists(int userId)
        {
            // Check if the user has a company by matching the UserId
            var company = await _context.Companies
                .FirstOrDefaultAsync(c => c.UserId == userId);  // Assumes that Company has UserId

            if (company == null)
            {
                return false;  // Return false if no company exists
            }

            return true;  // Return true if a company exists
        }

        // You could also return company details if necessary
        [HttpGet("get/{userId}")]
        public async Task<ActionResult<Company>> GetCompanyByUserId(int userId)
        {
            var company = await _context.Companies
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (company == null)
            {
                return NotFound("No company found for this user.");
            }

            return company;
        }

        // CompanyController.cs
        [HttpPost("create")]
        public async Task<ActionResult<Company>> CreateCompany([FromForm] CompanyCreateRequest request)
        {
            // Check if the user already has a company
            var existingCompany = await _context.Companies
                .FirstOrDefaultAsync(c => c.UserId == request.UserId);

            if (existingCompany != null)
            {
                return BadRequest("This user already has a company.");
            }

            // Map the incoming request to the company model
            var company = new Company
            {
                CompanyName = request.CompanyName,
                CompanyPhone = request.CompanyPhone,
                CompanyDescription = request.CompanyDescription,
                CompanyEmail = request.CompanyEmail,
                CompanyAddress = request.CompanyAddress,
                UserId = request.UserId,
                AverageRating = 0.0 // Initial average rating
            };

            // Handle avatar if it's provided
            if (request.CompanyAvatar != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await request.CompanyAvatar.CopyToAsync(memoryStream);
                    company.CompanyAvatar = memoryStream.ToArray();
                }
            }

            // Add to the database
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCompanyByUserId), new { userId = company.UserId }, company);
        }

        [HttpDelete("deleteByUser/{userId}")]
        public async Task<IActionResult> DeleteCompanyByUserId(int userId)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.UserId == userId);

            if (company == null)
            {
                return NotFound("Company not found for this user.");
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 No Content
        }

        [HttpPut("updateByUser/{userId}")]
        public async Task<IActionResult> UpdateCompanyByUserId(int userId, [FromForm] CompanyUpdateRequest request)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.UserId == userId);

            if (company == null)
            {
                return NotFound("Company not found for this user.");
            }

            // Update the company details
            company.CompanyName = request.CompanyName ?? company.CompanyName;
            company.CompanyPhone = request.CompanyPhone ?? company.CompanyPhone;
            company.CompanyEmail = request.CompanyEmail ?? company.CompanyEmail;
            company.CompanyDescription = request.CompanyDescription ?? company.CompanyDescription;
            company.CompanyAddress = request.CompanyAddress ?? company.CompanyAddress;

            // Handle avatar update if provided
            if (request.CompanyAvatar != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await request.CompanyAvatar.CopyToAsync(memoryStream);
                    company.CompanyAvatar = memoryStream.ToArray();
                }
            }

            await _context.SaveChangesAsync();

            return NoContent(); // Return 204 No Content
        }



    }
}
