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
        /// Get hero section (Public)
        /// </summary>
        [HttpGet("hero")]
        [AllowAnonymous]
        public async Task<ActionResult<HeroSectionDto>> GetHeroSection([FromQuery] string lang = "tr")
        {
            var query = new GetHeroSectionQuery(lang);
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound();
        }

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
        [HttpPut("hero/{id:guid}")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<ActionResult<HeroSectionDto>> UpdateHeroSection(Guid id, [FromBody] UpdateHeroSectionCommand command)
        {
            if (id != command.Id) return BadRequest("ID mismatch");

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        #endregion

        #region CtaSection

        /// <summary>
        /// Get CTA section (Public)
        /// </summary>
        [HttpGet("cta")]
        [AllowAnonymous]
        public async Task<ActionResult<CtaSectionDto>> GetCtaSection([FromQuery] string lang = "tr")
        {
            var query = new GetCtaSectionQuery(lang);
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound();
        }

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
        [HttpPut("cta/{id:guid}")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<ActionResult<CtaSectionDto>> UpdateCtaSection(Guid id, [FromBody] UpdateCtaSectionCommand command)
        {
            if (id != command.Id) return BadRequest("ID mismatch");

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        #endregion

        #region Features

        /// <summary>
        /// Get all features (Public)
        /// </summary>
        [HttpGet("features")]
        [AllowAnonymous]
        public async Task<ActionResult<List<FeatureDto>>> GetFeatures([FromQuery] string lang = "tr")
        {
            var query = new GetFeaturesQuery(lang);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

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
        [HttpPut("features/{id:guid}")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> UpdateFeature(Guid id, [FromBody] UpdateFeatureCommand command)
        {
            if (id != command.Id) return BadRequest("ID mismatch");

            await _mediator.Send(command);
            return NoContent();
        }

        #endregion

        #region InstagramPosts

        /// <summary>
        /// Get Instagram posts (Public)
        /// </summary>
        [HttpGet("instagram")]
        [AllowAnonymous]
        public async Task<ActionResult<List<InstagramPostDto>>> GetInstagramPosts([FromQuery] int count = 6)
        {
            var query = new GetInstagramPostsQuery(count);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

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
        [HttpPut("instagram/{id:guid}")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> UpdateInstagramPost(Guid id, [FromBody] UpdateInstagramPostCommand command)
        {
            if (id != command.Id) return BadRequest("ID mismatch");

            await _mediator.Send(command);
            return NoContent();
        }

        #endregion
    }
}
