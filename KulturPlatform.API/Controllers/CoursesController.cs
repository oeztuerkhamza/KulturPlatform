using KulturPlatform.Application.Commands.Course;
using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Queries.Course;
using KulturPlatform.Domain.Commons.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KulturPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CoursesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAll()
        {
            var query = new GetAllCoursesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CourseDto>> GetById(Guid id)
        {
            var query = new GetCourseByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateCourseCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCourseCommand command)
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
            var command = new DeleteCourseCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
