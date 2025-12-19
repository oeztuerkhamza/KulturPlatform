using KulturPlatform.Application.Commands.Imprint;
using KulturPlatform.Application.Dtos.ImprintDto;
using KulturPlatform.Application.Queries.Imprint;
using KulturPlatform.Domain.Commons.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KulturPlatform.API.Controllers
{
    [ApiController]
    [Route("api/imprint")]
    public class ImprintController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ImprintController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get imprint (Public)
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ImprintDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ImprintDto>> Get()
        {
            var imprint = await _mediator.Send(new GetImprintQuery());

            if (imprint is null)
                return NotFound(new { message = "Imprint not found." });

            return Ok(imprint);
        }

        /// <summary>
        /// Create imprint (UserAdmin+)
        /// </summary>
        [HttpPost]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateImprintCommand command)
        {
            var id = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(Get),
                new { id },
                new { id, message = "Imprint created successfully." }
            );
        }

        /// <summary>
        /// Update imprint (UserAdmin+)
        /// </summary>
        [HttpPut("{id:guid}")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateImprintDto dto)
        {
            var command = new UpdateImprintCommand(id, dto);
            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Delete imprint (SystemAdmin)
        /// </summary>
        [HttpDelete("{id:guid}")]
        [Authorize(Policy = Policies.RequireSystemAdmin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteImprintCommand(id));
            return NoContent();
        }
    }
}
