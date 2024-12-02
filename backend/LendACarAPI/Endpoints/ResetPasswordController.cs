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
            var user = await db.Users.FirstOrDefaultAsync(u => u.EmailAdress == resetDto.EmailAddress, cancellationToken);
            if (user != null)
            {

                var hashedPassword = GeneratePasswordHash(resetDto.NewPassword);

                var salt = hashedPassword.Item1;
                var hash = hashedPassword.Item2;

                for (var i = 0; i < hash.Length; i++)
                {
                    if (hash[i] == user.PasswordHash[i])
                        return BadRequest("Cannot reuse old password");
                }


                user.PasswordHash = hash;
                user.PasswordSalt = salt;

                await db.SaveChangesAsync(cancellationToken);

                return Ok(new { userType = "user" });
            }

            var employee = await db.Employees.FirstOrDefaultAsync(e => e.EmailAdress == resetDto.EmailAddress, cancellationToken);
            if (employee != null)
            {
                var hashedPassword = GeneratePasswordHash(resetDto.NewPassword);

                var salt = hashedPassword.Item1;
                var hash = hashedPassword.Item2;

                employee.PasswordHash = hash;
                employee.PasswordSalt = salt;

                await db.SaveChangesAsync(cancellationToken);

                return Ok ( new { userType = "employee" });
            }

            return NotFound();
        }

        [HttpPut("change")]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordResetDto resetDto, CancellationToken cancellationToken)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.EmailAdress == resetDto.EmailAddress, cancellationToken);
            if (user != null)
            {
                //if current password isn't null that means the user is changing his password if it is null then he forgot his password
                if (!string.IsNullOrWhiteSpace(resetDto.CurrentPassword))
                {
                    if (!ComparePasswords(resetDto.CurrentPassword, user))
                        return BadRequest("Entered password doesn't match current password");
                }
                else
                {
                    return BadRequest("Current password is required");
                }


                var hashedPassword = GeneratePasswordHash(resetDto.NewPassword);

                var salt = hashedPassword.Item1;
                var hash = hashedPassword.Item2;

                for (var i = 0; i < hash.Length; i++)
                {
                    if (hash[i] == user.PasswordHash[i])
                        return BadRequest("Cannot reuse old password");
                }


                user.PasswordHash = hash;
                user.PasswordSalt = salt;

                await db.SaveChangesAsync(cancellationToken);

                return Ok(new { userType = "user" });
            }

            return NotFound();
        }

        private (byte[], byte[]) GeneratePasswordHash(string newPassword)
        {
            var hmac=new HMACSHA256();

            var computedHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(newPassword));
            var salt = hmac.Key;
            return (salt, computedHash);
        }

        private bool ComparePasswords(string currentPassword, User user)
        {
            var hmac = new HMACSHA256(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(currentPassword));
            for (var i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                    return false;
            }

            return true;

        }
    }
}
