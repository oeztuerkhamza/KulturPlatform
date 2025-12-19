using KulturPlatform.Application.Commands.Partner;
using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Queries.Partner;
using KulturPlatform.Domain.Commons.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KulturPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartnersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PartnersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PartnerDto>>> GetAll()
        {
            var query = new GetAllPartnersQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PartnerDto>> GetById(Guid id)
        {
            var query = new GetPartnerByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreatePartnerCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePartnerCommand command)
        {
            if (id != command.Id)
                return BadRequest("Id mismatch.");

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.RequireSystemAdmin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeletePartnerCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
