using KulturPlatform.Application.Commands.DonatePage;
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

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateDonatePageCommand cmd)
        {
            if (id != cmd.Id) return BadRequest();

            var ok = await _mediator.Send(cmd);
            return ok ? Ok() : NotFound();
        }
    }

}
