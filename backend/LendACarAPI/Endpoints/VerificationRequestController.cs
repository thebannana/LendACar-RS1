using Azure.Core;
using LendACarAPI.Data;
using LendACarAPI.Data.Models;
using LendACarAPI.DTOs;
using LendACarAPI.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace LendACarAPI.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationRequestController(ApplicationDbContext db) : ControllerBase
    {
        [HttpGet("get")]
        public async Task<ActionResult<VehicleCategory[]>> GetAllVerificationRequests()
        {
            var requests = await db.VerificationRequests.ToArrayAsync();
            return Ok(requests);
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetVerificationRequestById(int id)
        {
            var request = await db.VerificationRequests.FirstOrDefaultAsync(vr => vr.Id == id);

            if (request == null) return NotFound("Verification request not found");

            var verificationRequest = new VerificationDto
            {
                Id = request.Id,
                RequestDate = request.RequestDate,
                UserId = request.UserId,
                RequestComment = request.RequestComment,
                EmployeeId = request.EmployeeId,
            };

            return Ok(verificationRequest);
        }

        [HttpPost("addNew")]
        public async Task<IActionResult> AddVerificationRequest([FromBody] VerificationSubmitDto verificationSubmitDto)
        {
            if (await VerificationRequestExists(verificationSubmitDto.UserId))
                return BadRequest("User already submited a verification request!");

            var newVerificationRequest = new VerificationRequest
            {
                RequestDate = verificationSubmitDto.RequestDate,
                UserId = verificationSubmitDto.UserId,
                RequestComment = verificationSubmitDto.RequestComment,
            };

            db.VerificationRequests.Add(newVerificationRequest);
            await db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVerificationRequestById), new { id = newVerificationRequest.Id }, verificationSubmitDto);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> ConfirmVerificationRequest(int id, [FromBody] VerificationDto approvedVerificationRequest)
        {
            var verificationRequest = await db.VerificationRequests.FirstOrDefaultAsync(vr => vr.Id == id);
            if (verificationRequest == null) return NotFound("Verification request not found");

            var userGettingVerified = await db.Users.FirstOrDefaultAsync(u => u.Id == verificationRequest.UserId);
            if (userGettingVerified == null) return NotFound("User not found");

            verificationRequest.EmployeeId = approvedVerificationRequest.EmployeeId;
            userGettingVerified.IsVerified = true;

            await db.SaveChangesAsync();

            return Ok(new VerificationDto
            {
                Id = verificationRequest.Id,
                RequestDate = verificationRequest.RequestDate,
                UserId = verificationRequest.UserId,
                RequestComment = verificationRequest.RequestComment,
                EmployeeId = verificationRequest.EmployeeId,
            });
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> DeleteVerificationRequest(int id)
        {
            var verificationRequest = await db.VerificationRequests.FirstOrDefaultAsync(vr => vr.Id == id);

            if (verificationRequest == null) return NotFound("Verification request not found");

            db.VerificationRequests.Remove(verificationRequest);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> VerificationRequestExists(int userId)
        {
            return await db.VerificationRequests.AnyAsync(vr => vr.UserId == userId);
        }
    }

}
