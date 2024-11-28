using LendACarAPI.Data;
using LendACarAPI.Data.Models;
using LendACarAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace LendACarAPI.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]

    public class ResetPasswordController(ApplicationDbContext db) : ControllerBase
    {
        /// <summary>
        /// Global controller for reseting password for both users or employees
        /// It checks thru the email if it is a user or employee beacuse employees have @lendacar email and it is unique for every employee
        /// </summary>
        /// <param name="resetDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// It returns employee or user because of the frontedn routing to send the user to the correct login page
        /// </returns>
        [HttpPut("reset")]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordResetDto resetDto,CancellationToken cancellationToken)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.EmailAdress == resetDto.emailAddress, cancellationToken);
            if (user != null)
            {
                var hashedPassword = GeneratePasswordHash(resetDto.password);

                var salt = hashedPassword.Item1;
                var hash = hashedPassword.Item2;

                user.PasswordHash = hash;
                user.PasswordSalt = salt;

                await db.SaveChangesAsync(cancellationToken);

                return Ok(new { userType = "user" });
            }

            var employee = await db.Employees.FirstOrDefaultAsync(e => e.EmailAdress == resetDto.emailAddress, cancellationToken);
            if (employee != null)
            {
                var hashedPassword = GeneratePasswordHash(resetDto.password);

                var salt = hashedPassword.Item1;
                var hash = hashedPassword.Item2;

                employee.PasswordHash = hash;
                employee.PasswordSalt = salt;

                await db.SaveChangesAsync(cancellationToken);

                return Ok ( new { userType = "employee" });
            }

            return NotFound();
        }


        private (byte[], byte[]) GeneratePasswordHash(string password)
        {
            var hmac=new HMACSHA256();

            var computedHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            var salt = hmac.Key;
            return (salt, computedHash);
        }
    }
}
