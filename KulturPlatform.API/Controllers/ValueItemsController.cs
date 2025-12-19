using KulturPlatform.Application.Commands.ValueItem;
using KulturPlatform.Application.Dtos.NewFolder;
using KulturPlatform.Application.Queries.ValueItem;
using KulturPlatform.Domain.Commons.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KulturPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValueItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ValueItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/ValueItems
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IReadOnlyList<ValueItemDetailDto>>> GetAll()
        {
            var query = new GetAllValueItemsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/ValueItems/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ValueItemDetailDto>> GetById(Guid id)
        {
            var query = new GetValueItemByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/ValueItems
        [HttpPost]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateValueItemCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        // PUT: api/ValueItems/{id}
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateValueItemCommand command)
        {
            // Accept route id as source of truth. Ensure command uses route id to avoid mismatch when client omits Id.
            var commandWithId = command with { Id = id };

            await _mediator.Send(commandWithId);
            return NoContent();
        }

        // DELETE: api/ValueItems/{id}
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.RequireSystemAdmin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteValueItemCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
