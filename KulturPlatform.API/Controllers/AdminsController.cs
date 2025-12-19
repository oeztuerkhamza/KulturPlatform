using KulturPlatform.Application.Commands.Admin;
using KulturPlatform.Application.Dtos.AdminDto;
using KulturPlatform.Application.Queries.Admin;
using KulturPlatform.Domain.Commons.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KulturPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // All endpoints require authentication
    public class AdminsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/admins - SystemAdmin only
        [HttpGet]
        [Authorize(Policy = Policies.RequireSystemAdmin)]
        public async Task<ActionResult<IEnumerable<AdminDto>>> GetAll()
        {
            var query = new GetAllAdminsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/admins/{id} - SystemAdmin or self
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminDto>> GetById(Guid id)
        {
            // Users can view their own profile, SystemAdmin can view all
            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var isSystemAdmin = User.IsInRole(Roles.SystemAdmin);

            if (!isSystemAdmin && currentUserId != id.ToString())
                return Forbid();

            var query = new GetAdminByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // GET: api/admins/me - Current user profile
        [HttpGet("me")]
        public async Task<ActionResult<AdminDto>> GetMe()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var id))
                return Unauthorized();

            var query = new GetAdminByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/admins - SystemAdmin only
        [HttpPost]
        [Authorize(Policy = Policies.RequireSystemAdmin)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateAdminCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        // PUT: api/admins/{id} - SystemAdmin only
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.RequireSystemAdmin)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAdminCommand command)
        {
            if (id != command.Id)
                return BadRequest("Id mismatch.");

            await _mediator.Send(command);
            return NoContent();
        }

        // PUT: api/admins/{id}/role - SystemAdmin only
        [HttpPut("{id}/role")]
        [Authorize(Policy = Policies.RequireSystemAdmin)]
        public async Task<IActionResult> UpdateRole(Guid id, [FromBody] UpdateAdminRoleCommand command)
        {
            if (id != command.AdminId)
                return BadRequest("Id mismatch.");

            await _mediator.Send(command);
            return NoContent();
        }

        // PUT: api/admins/{id}/activate - SystemAdmin only
        [HttpPut("{id}/activate")]
        [Authorize(Policy = Policies.RequireSystemAdmin)]
        public async Task<IActionResult> Activate(Guid id)
        {
            var command = new ActivateAdminCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }

        // PUT: api/admins/{id}/deactivate - SystemAdmin only
        [HttpPut("{id}/deactivate")]
        [Authorize(Policy = Policies.RequireSystemAdmin)]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            var command = new DeactivateAdminCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE: api/admins/{id} - SystemAdmin only
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.RequireSystemAdmin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Prevent self-deletion
            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (currentUserId == id.ToString())
                return BadRequest("Cannot delete your own account.");

            var command = new DeleteAdminCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
