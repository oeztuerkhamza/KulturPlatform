using KulturPlatform.Application.Commands.Auth;
using KulturPlatform.Application.Dtos.AuthDto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KulturPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto loginDto)
        {
            var command = new LoginCommand(loginDto.Email, loginDto.Password);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            var adminIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (adminIdClaim == null || !Guid.TryParse(adminIdClaim.Value, out var adminId))
                return Unauthorized();

            var command = new ChangePasswordCommand(adminId, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            await _mediator.Send(command);
            return Ok(new { message = "Password changed successfully." });
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var alternativeRole = User.FindFirst("role")?.Value;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(userId))
                return Unauthorized();

            // Debug: T�m claim'leri d�nd�r
            var allClaims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();

            return Ok(new
            {
                Id = userId,
                Email = email,
                Role = role,
                AlternativeRole = alternativeRole,
                AllClaims = allClaims
            });
        }

        /// <summary>
        /// Debug endpoint to check all token claims
        /// </summary>
        [Authorize]
        [HttpGet("debug/claims")]
        public IActionResult GetClaims()
        {
            var claims = User.Claims.Select(c => new
            {
                Type = c.Type,
                Value = c.Value,
                Issuer = c.Issuer
            }).ToList();

            var identity = User.Identity;
            var isAuthenticated = identity?.IsAuthenticated ?? false;
            var authenticationType = identity?.AuthenticationType;

            return Ok(new
            {
                IsAuthenticated = isAuthenticated,
                AuthenticationType = authenticationType,
                Claims = claims,
                Roles = User.Claims
                    .Where(c => c.Type == ClaimTypes.Role || c.Type == "role")
                    .Select(c => c.Value)
                    .ToList()
            });
        }
    }
}
