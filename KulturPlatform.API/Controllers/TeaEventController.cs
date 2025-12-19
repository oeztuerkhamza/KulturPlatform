using KulturPlatform.Application.Commands.TeaEvent;
using KulturPlatform.Application.Dtos.TeaEventDto;
using KulturPlatform.Application.Queries.TeaEvent;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KulturPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeaEventController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeaEventController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // 🔹 GET by Id (public)
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<TeaEventDto>> GetById(Guid id)
        {
            var teaEvent = await _mediator.Send(new GetTeaEventByIdQuery(id));
            if (teaEvent == null)
                return NotFound(new { message = $"TeaEvent with Id '{id}' not found." });

            return Ok(teaEvent);
        }

        // 🔹 GET All (admin)
        [HttpGet]
        // [Authorize(Policy = "RequireUserAdmin")]
        public async Task<ActionResult<IEnumerable<TeaEventDto>>> GetAll()
        {
            var teaEvents = await _mediator.Send(new GetAllTeaEventQuery());
            return Ok(teaEvents);
        }

        // 🔹 CREATE
        [HttpPost]
        [Authorize(Policy = "RequireUserAdmin")]
        public async Task<ActionResult> Create([FromBody] CreateTeaEventCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = id }, null);
        }

        // 🔹 UPDATE by Id
        [HttpPut("{id:guid}")]
        [Authorize(Policy = "RequireUserAdmin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTeaEventCommand command)
        {
            if (id != command.Id)
                return BadRequest(new { message = "Id in URL does not match Id in request body." });

            await _mediator.Send(command);
            return NoContent();
        }

        // 🔹 DELETE by Id
        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "RequireSystemAdmin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteTeaEventCommand(id));
            return NoContent();
        }
    }
}
