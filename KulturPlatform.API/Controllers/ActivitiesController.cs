using KulturPlatform.Application.Commands.Activity;
using KulturPlatform.Application.Dtos;
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

        // GET: api/activities - Public (Anyone can view)
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ActivityDto>>> GetAll()
        {
            var query = new GetAllActivitiesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/activities/upcoming - Public (Anyone can view)
        [HttpGet("upcoming")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ActivityDto>>> GetUpcoming()
        {
            var query = new GetUpcomingActivitiesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/activities/{id} - Public (Anyone can view)
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ActivityDto>> GetById(Guid id)
        {
            var query = new GetActivityByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/activities - UserAdmin+ (Create content)
        [HttpPost]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateActivityCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        // PUT: api/activities/{id} - UserAdmin+ (Update content)
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateActivityCommand command)
        {
            if (id != command.Id)
                return BadRequest("Id mismatch.");

            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE: api/activities/{id} - SystemAdmin only (Destructive operation)
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.RequireSystemAdmin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteActivityCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
