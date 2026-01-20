using KulturPlatform.Application.Commands.Activity;
using KulturPlatform.Application.Dtos.Activity;
using KulturPlatform.Application.Queries.Activity;
using KulturPlatform.Domain.Commons.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KulturPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ActivitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all activities
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ActivityDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllActivitiesQuery());
            return Ok(result);
        }

        /// <summary>
        /// Get upcoming activities
        /// </summary>
        [HttpGet("upcoming")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ActivityDto>>> GetUpcoming()
        {
            var result = await _mediator.Send(new GetUpcomingActivitiesQuery());
            return Ok(result);
        }

        /// <summary>
        /// Get activity by ID
        /// </summary>
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ActivityDto>> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetActivityByIdQuery(id));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Create a new activity (UserAdmin+)
        /// Supports both URL-based and database-stored images:
        /// - For URL: Provide ImageUrl field
        /// - For Database: Provide ImageBase64 (with or without data URI prefix) and ImageFileName
        /// Images are automatically compressed and optimized (max 1920x1080, quality 85)
        /// </summary>
        [HttpPost]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<ActionResult<Guid>> Create(
            [FromBody] CreateActivityCommand command)
        {
            var id = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                id
            );
        }

        /// <summary>
        /// Update an existing activity (UserAdmin+)
        /// Supports both URL-based and database-stored images:
        /// - For URL: Provide ImageUrl field
        /// - For Database: Provide ImageBase64 (with or without data URI prefix) and ImageFileName
        /// Images are automatically compressed and optimized (max 1920x1080, quality 85)
        /// </summary>
        [HttpPut("{id:guid}")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateActivityCommand command)
        {
            if (id != command.Id)
                return BadRequest("Id mismatch.");

            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Delete an activity (SystemAdmin only)
        /// </summary>
        [HttpDelete("{id:guid}")]
        [Authorize(Policy = Policies.RequireSystemAdmin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteActivityCommand(id));
            return NoContent();
        }
    }
}
