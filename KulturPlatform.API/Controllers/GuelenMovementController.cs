using KulturPlatform.Application.Commands.GuelenMovement;
using KulturPlatform.Application.Queries.GuelenMovement;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KulturPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuelenMovementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GuelenMovementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllGuelenMovementsQuery());
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetGuelenMovementByIdQuery(id));
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGuelenMovementCommand command)
        {
            var newId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = newId }, null);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateGuelenMovementCommand command)
        {
            if (id != command.Id) return BadRequest();

            var success = await _mediator.Send(command);
            return success ? Ok() : NotFound();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _mediator.Send(new DeleteGuelenMovementCommand(id));
            return success ? Ok() : NotFound();
        }
    }

}
