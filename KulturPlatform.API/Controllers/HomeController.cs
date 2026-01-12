using KulturPlatform.Application.Commands.Home;
using KulturPlatform.Application.Dtos.Home;
using KulturPlatform.Application.Queries.Home;
using KulturPlatform.Domain.Commons.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KulturPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get complete home page data (Public)
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<HomeDto>> GetHomePage([FromQuery] string lang = "tr")
        {
            var query = new GetHomePageQuery(lang);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        #region HeroSection

        /// <summary>
        /// Create hero section (SystemAdmin only)
        /// </summary>
        [HttpPost("hero")]
        [Authorize(Policy = Policies.RequireSystemAdmin)]
        public async Task<ActionResult<Guid>> CreateHeroSection([FromBody] CreateHeroSectionCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        /// <summary>
        /// Update hero section (UserAdmin+)
        /// </summary>
        [HttpPut("hero")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> UpdateHeroSection([FromBody] UpdateHeroSectionCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        #endregion

        #region CtaSection

        /// <summary>
        /// Create CTA section (SystemAdmin only)
        /// </summary>
        [HttpPost("cta")]
        [Authorize(Policy = Policies.RequireSystemAdmin)]
        public async Task<ActionResult<Guid>> CreateCtaSection([FromBody] CreateCtaSectionCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        /// <summary>
        /// Update CTA section (UserAdmin+)
        /// </summary>
        [HttpPut("cta")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> UpdateCtaSection([FromBody] UpdateCtaSectionCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        #endregion

        #region Features

        /// <summary>
        /// Create feature (UserAdmin+)
        /// </summary>
        [HttpPost("features")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<ActionResult<Guid>> CreateFeature([FromBody] CreateFeatureCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        /// <summary>
        /// Update feature (UserAdmin+)
        /// </summary>
        [HttpPut("features")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> UpdateFeature([FromBody] UpdateFeatureCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        #endregion

        #region InstagramPosts

        /// <summary>
        /// Create Instagram post (UserAdmin+)
        /// </summary>
        [HttpPost("instagram")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<ActionResult<Guid>> CreateInstagramPost([FromBody] CreateInstagramPostCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        /// <summary>
        /// Update Instagram post (UserAdmin+)
        /// </summary>
        [HttpPut("instagram")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> UpdateInstagramPost([FromBody] UpdateInstagramPostCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        #endregion
    }
}
