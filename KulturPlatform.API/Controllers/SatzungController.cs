using KulturPlatform.Application.Commands.Satzung;
using KulturPlatform.Application.Dtos.SatzungDto;
using KulturPlatform.Application.Queries.Satzung;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KulturPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SatzungController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SatzungController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // 🔹 GET All (admin)
        [HttpGet]
        // [Authorize(Policy = "RequireUserAdmin")]
        public async Task<ActionResult<IEnumerable<SatzungDto>>> GetAll()
        {
            var satzungen = await _mediator.Send(new GetAllSatzungQuery());
            return Ok(satzungen);
        }

        // 🔹 GET by Id (admin)
        [HttpGet("{id:guid}")]
        [Authorize(Policy = "RequireUserAdmin")]
        public async Task<ActionResult<SatzungDto>> GetById(Guid id)
        {
            var satzung = await _mediator.Send(new GetSatzungByIdQuery(id));
            if (satzung == null) return NotFound();
            return Ok(satzung);
        }

        // 🔹 CREATE
        [HttpPost]
        [Authorize(Policy = "RequireUserAdmin")]
        public async Task<ActionResult> Create([FromBody] CreateSatzungCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = id }, null);
        }

        // 🔹 UPDATE by Id
        [HttpPut("{id:guid}")]
        [Authorize(Policy = "RequireUserAdmin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSatzungCommand command)
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
            await _mediator.Send(new DeleteSatzungCommand(id));
            return NoContent();
        }
    }
}
