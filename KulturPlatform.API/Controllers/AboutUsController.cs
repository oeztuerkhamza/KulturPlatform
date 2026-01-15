using KulturPlatform.Application.Commands.AboutUs;
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

        /// <summary>
        /// Get complete About Us aggregate with all IDs (Public)
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAggregate()
            => Ok(await _mediator.Send(new GetAboutUsAggregateQuery()));

        #region Quote

        [HttpPost("quote")]
        [Authorize(Policy = Policies.RequireSystemAdmin)]
        public async Task<IActionResult> CreateQuote([FromBody] CreateAboutUsQuoteCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { Id = id, Message = "Quote created successfully" });
        }

        [HttpPut("quote/{id}")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> UpdateQuote(Guid id, [FromBody] UpdateAboutUsQuoteCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            await _mediator.Send(command);
            return NoContent();
        }

        #endregion

        #region WhoWeAre

        [HttpPost("who-we-are")]
        [Authorize(Policy = Policies.RequireSystemAdmin)]
        public async Task<IActionResult> CreateWhoWeAre([FromBody] CreateAboutUsWhoWeAreCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { Id = id, Message = "Who We Are created successfully" });
        }

        [HttpPut("who-we-are/{id}")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> UpdateWhoWeAre(Guid id, [FromBody] UpdateAboutUsWhoWeAreCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            await _mediator.Send(command);
            return NoContent();
        }

        #endregion

        #region Goals

        [HttpPost("goals")]
        [Authorize(Policy = Policies.RequireSystemAdmin)]
        public async Task<IActionResult> CreateGoals([FromBody] CreateAboutUsGoalsCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { Id = id, Message = "Goals created successfully" });
        }

        [HttpPut("goals/{id}")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> UpdateGoals(Guid id, [FromBody] UpdateAboutUsGoalsCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            await _mediator.Send(command);
            return NoContent();
        }

        #endregion

        #region Vision

        [HttpPost("vision")]
        [Authorize(Policy = Policies.RequireSystemAdmin)]
        public async Task<IActionResult> CreateVision([FromBody] CreateAboutUsVisionCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { Id = id, Message = "Vision created successfully" });
        }

        [HttpPut("vision/{id}")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> UpdateVision(Guid id, [FromBody] UpdateAboutUsVisionCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            await _mediator.Send(command);
            return NoContent();
        }

        #endregion

        #region Mission

        [HttpPost("mission")]
        [Authorize(Policy = Policies.RequireSystemAdmin)]
        public async Task<IActionResult> CreateMission([FromBody] CreateAboutUsMissionCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { Id = id, Message = "Mission created successfully" });
        }

        [HttpPut("mission/{id}")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> UpdateMission(Guid id, [FromBody] UpdateAboutUsMissionCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            await _mediator.Send(command);
            return NoContent();
        }

        #endregion

        #region Core Values

        [HttpPost("core-values")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> CreateCoreValue([FromBody] CreateCoreValueCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { Id = id, Message = "Core value created successfully" });
        }

        [HttpPut("core-values/{id}")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> UpdateCoreValue(Guid id, [FromBody] UpdateCoreValueCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("core-values/{id}")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> DeleteCoreValue(Guid id)
        {
            await _mediator.Send(new DeleteCoreValueCommand(id));
            return NoContent();
        }

        #endregion

        #region Focus Areas

        [HttpPost("focus-areas")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> CreateFocusArea([FromBody] CreateFocusAreaCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { Id = id, Message = "Focus area created successfully" });
        }

        [HttpPut("focus-areas/{id}")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> UpdateFocusArea(Guid id, [FromBody] UpdateFocusAreaCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("focus-areas/{id}")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> DeleteFocusArea(Guid id)
        {
            await _mediator.Send(new DeleteFocusAreaCommand(id));
            return NoContent();
        }

        #endregion

        #region Activity Areas

        [HttpPost("activity-areas")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> CreateActivityArea([FromBody] CreateActivityAreaCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { Id = id, Message = "Activity area created successfully" });
        }

        [HttpPut("activity-areas/{id}")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> UpdateActivityArea(Guid id, [FromBody] UpdateActivityAreaCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("activity-areas/{id}")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> DeleteActivityArea(Guid id)
        {
            await _mediator.Send(new DeleteActivityAreaCommand(id));
            return NoContent();
        }

        #endregion

        #region Team Members

        [HttpPost("team-members")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> CreateTeamMember([FromBody] CreateTeamMemberCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { Id = id, Message = "Team member created successfully" });
        }

        [HttpPut("team-members/{id}")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> UpdateTeamMember(Guid id, [FromBody] UpdateTeamMemberCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("team-members/{id}")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> DeleteTeamMember(Guid id)
        {
            await _mediator.Send(new DeleteTeamMemberCommand(id));
            return NoContent();
        }

        #endregion

        #region Human Rights

        /// <summary>
        /// Get Human Rights section (latest record)
        /// </summary>
        [HttpGet("human-rights")]
        [AllowAnonymous]
        public async Task<IActionResult> GetHumanRights()
        {
            var result = await _mediator.Send(new GetAboutUsHumanRightsQuery());
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Get Human Rights section by ID
        /// </summary>
        [HttpGet("human-rights/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetHumanRightsById(Guid id)
        {
            var result = await _mediator.Send(new GetAboutUsHumanRightsByIdQuery(id));
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost("human-rights")]
        [Authorize(Policy = Policies.RequireSystemAdmin)]
        public async Task<IActionResult> CreateHumanRights([FromBody] CreateAboutUsHumanRightsCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetHumanRightsById), new { id }, new { Id = id, Message = "Human Rights section created successfully" });
        }

        [HttpPut("human-rights/{id}")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> UpdateHumanRights(Guid id, [FromBody] UpdateAboutUsHumanRightsCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            await _mediator.Send(command);
            return NoContent();
        }

        #endregion
    }
}
