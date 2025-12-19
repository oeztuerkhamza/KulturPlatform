using KulturPlatform.Application.Commands.AboutUs;
using KulturPlatform.Application.Dtos.AboutUs;
using KulturPlatform.Application.Queries.AboutUs;
using KulturPlatform.Domain.Commons.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KulturPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AboutUsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AboutUsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
            => Ok(await _mediator.Send(new GetAboutUsQuery()));

        [HttpPut]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> Update(AboutUsDto dto)
        {
            await _mediator.Send(new UpdateAboutUsCommand(dto));
            return NoContent();
        }
        [HttpPost]
        [Authorize(Policy = Policies.RequireSystemAdmin)]
        public async Task<IActionResult> CreateOrUpdate([FromBody] AboutUsDto dto)
        {
            if (dto == null)
                return BadRequest("DTO cannot be null");

            var command = new CreateOrUpdateAboutUsCommand(dto);
            await _mediator.Send(command);

            return Ok(new { Message = "About Us content saved successfully" });
        }
    }
}
