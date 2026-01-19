using KulturPlatform.Application.Commands.ContactMessages;
using KulturPlatform.Application.Dtos.ContactMessages;
using KulturPlatform.Application.Queries.ContactMessages;
using KulturPlatform.Domain.Commons.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KulturPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactMessagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactMessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all contact messages (UserAdmin+)
        /// </summary>
        [HttpGet]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<ActionResult<IEnumerable<ContactMessageDto>>> GetAll()
        {
            var query = new GetAllContactMessagesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get contact message by ID (UserAdmin+)
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<ActionResult<ContactMessageDto>> GetById(Guid id)
        {
            var query = new GetContactMessageByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Submit contact message (Public)
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateContactMessageCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        /// <summary>
        /// Mark message as read (UserAdmin+)
        /// </summary>
        [HttpPatch("{id}/mark-as-read")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> MarkAsRead(Guid id)
        {
            var command = new MarkContactMessageAsReadCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Delete contact message (SystemAdmin only)
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.RequireSystemAdmin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteContactMessageCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
