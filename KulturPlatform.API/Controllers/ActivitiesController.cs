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

        // GET: api/activities
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ActivityDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllActivitiesQuery());
            return Ok(result);
        }

        // GET: api/activities/upcoming
        [HttpGet("upcoming")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ActivityDto>>> GetUpcoming()
        {
            var result = await _mediator.Send(new GetUpcomingActivitiesQuery());
            return Ok(result);
        }

        // GET: api/activities/{id}
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ActivityDto>> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetActivityByIdQuery(id));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/activities
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

        // PUT: api/activities/{id}
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

        // DELETE: api/activities/{id}
        [HttpDelete("{id:guid}")]
        [Authorize(Policy = Policies.RequireSystemAdmin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteActivityCommand(id));
            return NoContent();
        }
    }
}
