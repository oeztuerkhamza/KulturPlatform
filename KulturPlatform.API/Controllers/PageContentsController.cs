using KulturPlatform.Application.Commands.PageContent;
using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Queries.PageContent;
using KulturPlatform.Domain.Commons.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KulturPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PageContentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PageContentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PageContentDto>>> GetAll()
        {
            var query = new GetAllPageContentsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PageContentDto>> GetById(Guid id)
        {
            var query = new GetPageContentByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("page/{pageName}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PageContentDto>>> GetByPageName(string pageName)
        {
            var query = new GetPageContentsByPageNameQuery(pageName);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreatePageContentCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePageContentCommand command)
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
            var command = new DeletePageContentCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
