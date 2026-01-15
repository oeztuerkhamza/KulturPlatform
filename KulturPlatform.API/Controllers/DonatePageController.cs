using KulturPlatform.Application.Commands.DonatePage;
using KulturPlatform.Application.Queries.DonatePage;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KulturPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonatePageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DonatePageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetDonatePageQuery());
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDonatePageCommand cmd)
        {
            var id = await _mediator.Send(cmd);
            return CreatedAtAction(nameof(Get), new { id }, new { id });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateDonatePageCommand cmd)
        {
            if (id != cmd.Id) return BadRequest();

            var ok = await _mediator.Send(cmd);
            return ok ? Ok() : NotFound();
        }
    }
}
