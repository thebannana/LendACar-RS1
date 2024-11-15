using LendACarAPI.Data;
using LendACarAPI.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LendACarAPI.Endpoints.CityEndpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController(ApplicationDbContext db) : ControllerBase
    {
        [HttpGet("get")]
        public async Task<ActionResult<City[]>> GetAllCities()
        {
            var cities=await db.Cities.Include(c => c.Country).OrderBy(c=>c.Country.Name).ToArrayAsync();

            return Ok(cities);
        }
    }
}
