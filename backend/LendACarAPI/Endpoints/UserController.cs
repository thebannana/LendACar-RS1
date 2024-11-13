using LendACarAPI.Data;
using LendACarAPI.Data.Models;
using LendACarAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace LendACarAPI.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(ApplicationDbContext db) : ControllerBase
    {

        [HttpGet("getById/{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var user= await db.Users
                .Include(u=>u.City)
                .Include(u => u.City != null ? u.City.Country : null)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) throw new Exception("User not found");

            var userDto = new UserDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                BirthDate = user.BirthDate,
                City = user.City,
                CityId = user.CityId,
                EmailAddress = user.EmailAdress,
                Username=user.Username,
                AverageRating = user.AverageRating,
            };

            return Ok(userDto);
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> RegisterUser([FromBody]RegisterUserDto user)
        {
            if (await UserExists(user.Username, user.EmailAdress)) return BadRequest("Username or email is taken");

            var createdUser = new User()
            {
                FirstName= user.FirstName,
                LastName= user.LastName,
                PhoneNumber= user.PhoneNumber,
                BirthDate = user.BirthDate,
                CityId = user.CityId,
                EmailAdress = user.EmailAdress,
                Username=user.Username,
                Password=user.Password,
                CreatedDate=DateTime.Now,
            };

            db.Users.Add(createdUser);
            await db.SaveChangesAsync();    

            return Created();
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LoginUser([FromBody] LoginUserDto userLogin)
        {
            var user = await db.Users
                .Include(u=>u.City)
                .Include(u => u.City != null ? u.City.Country : null)
                .FirstOrDefaultAsync(u => u.Username.ToLower() == userLogin.Username.ToLower()
            && u.Password == userLogin.Password);

            if (user == null) return Unauthorized("Invalid credentials");

            return Ok(new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                BirthDate = user.BirthDate,
                City = user.City,
                CityId = user.CityId,
                EmailAddress = user.EmailAdress,
                Username = user.Username,
                AverageRating = user.AverageRating,
            });
        }



        [HttpPut("update/{id}")]
        public async Task<IActionResult> EditUser([FromBody]UserDto updatedUser,int id)
        {
            var user = await db.Users
            .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return NotFound("User not found");

            user.FirstName = updatedUser.FirstName; 
            user.LastName = updatedUser.LastName;
            user.EmailAdress = updatedUser.EmailAddress;
            user.PhoneNumber= updatedUser.PhoneNumber;
            user.CityId = updatedUser.CityId;

            await db.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("remove/{id}")]
        public async Task<ActionResult<string>> RemoveUser(int id)
        {
            var user = await db.Users
            .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return NotFound("User not found");

            db.Remove(user);
            await db.SaveChangesAsync();

            return Ok($"User with the username {user.Username} was removed");
        }


        private async Task<bool> UserExists(string username,string email)
        {
            return await db.Users.AnyAsync(user => user.Username.ToLower() == username.ToLower()
            && user.EmailAdress.ToLower()==email.ToLower());
        }
    }
}
