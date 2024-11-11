using LendACarAPI.Data;
using LendACarAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace LendACarAPI.Endpoints.DataSeedEndpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataSeedGenerateEndpoint(ApplicationDbContext db):ControllerBase
    {

        [HttpPost]
        public async Task<string> DataSeedGeneration(CancellationToken cancellationToken = default)
        {
            // Kreiranje država
            var countries = new List<Country>
            {
                new Country { Name = "Bosnia and Herzegovina" },
                new Country { Name = "Croatia" },
                new Country { Name = "Germany" },
                new Country { Name = "Austria" },
                new Country { Name = "USA" }
            };

            // Kreiranje gradova
            var cities = new List<City>
            {
                new City { Name = "Sarajevo", Country = countries[0] },
                new City { Name = "Mostar", Country = countries[0] },
                new City { Name = "Tuzla", Country = countries[0] },
                new City { Name = "Konjic", Country = countries[0] },
                new City { Name = "Jablanica", Country = countries[0] },
                new City { Name = "Zagreb", Country = countries[1] },
                new City { Name = "Berlin", Country = countries[2] },
                new City { Name = "Vienna", Country = countries[3] },
                new City { Name = "Klagenfurt", Country = countries[3] },
                new City { Name = "Graz", Country = countries[3] },
                new City { Name = "New York", Country = countries[4] },
                new City { Name = "Los Angeles", Country = countries[4] }
            };

            // Kreiranje korisnika s ulogama
            var users = new List<User>
            {
                new User
                {
                    Username = "KalloX",
                    Password = "password",
                    FirstName = "Denis",
                    LastName = "Kundo",
                    BirthDate = new DateTime(2003,11,11),
                    CreatedDate = DateTime.Now,
                    Country = countries[0],
                    PhoneNumber="123-456-7890",
                    EmailAdress="denis@edu.fit.ba"
                },

                new User
                {
                    Username = "LedoSlav",
                    Password = "password",
                    FirstName = "Edin",
                    LastName = "Tabak",
                    BirthDate =new DateTime(2003,11,10),
                    CreatedDate = DateTime.Now,
                    Country = countries[0],
                    PhoneNumber="123-456-7890",
                    EmailAdress="edin@edu.fit.ba"

                },

                new User
                {
                    Username = "Vaha",
                    Password = "password",
                    FirstName = "Emin", 
                    LastName = "Brankovic",
                    BirthDate = new DateTime(2002,10,31),
                    CreatedDate = DateTime.Now,
                    Country = countries[0],
                    PhoneNumber="123-456-7890",
                    EmailAdress="emin@edu.fit.ba"

                },
            };

            // Dodavanje podataka u bazu
            await db.Countries.AddRangeAsync(countries, cancellationToken);
            await db.Cities.AddRangeAsync(cities, cancellationToken);
            await db.Users.AddRangeAsync(users, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);

            return "Data generation completed successfully.";
        }
    }

}

