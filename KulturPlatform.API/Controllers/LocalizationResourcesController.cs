using KulturPlatform.Application.Commands.LocalizationResource;
using KulturPlatform.Application.Dtos.LocalizationDto;
using KulturPlatform.Application.Queries.LocalizationResource;
using KulturPlatform.Domain.Commons.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KulturPlatform.API.Controllers
{
    /// <summary>
    /// Localization Resources Management (Admin Panel)
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = Policies.RequireUserAdmin)]
    public class LocalizationResourcesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LocalizationResourcesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all localization resources (UserAdmin+)
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<LocalizationResourceDto>>> GetAll()
        {
            var query = new GetAllLocalizationResourcesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get localization resource by ID (UserAdmin+)
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<LocalizationResourceDto>> GetById(Guid id)
        {
            var query = new GetLocalizationResourceByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Get localization resources by section (UserAdmin+)
        /// </summary>
        [HttpGet("section/{section}")]
        public async Task<ActionResult<List<LocalizationResourceDto>>> GetBySection(string section)
        {
            var query = new GetLocalizationResourcesBySectionQuery(section);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Create new localization resource (UserAdmin+)
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateLocalizationResourceDto dto)
        {
            var command = new CreateLocalizationResourceCommand(
                Key: dto.Key,
                Turkish: dto.Turkish,
                German: dto.German,
                English: dto.English,
                Section: dto.Section,
                Description: dto.Description
            );

            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        /// <summary>
        /// Update localization resource (UserAdmin+)
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLocalizationResourceDto dto)
        {
            var command = new UpdateLocalizationResourceCommand(
                Id: id,
                Turkish: dto.Turkish,
                German: dto.German,
                English: dto.English,
                Description: dto.Description
            );

            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Activate localization resource (UserAdmin+)
        /// </summary>
        [HttpPatch("{id}/activate")]
        public async Task<IActionResult> Activate(Guid id)
        {
            var command = new ActivateLocalizationResourceCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deactivate localization resource (UserAdmin+)
        /// </summary>
        [HttpPatch("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            var command = new DeactivateLocalizationResourceCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Delete localization resource (SystemAdmin only)
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.RequireSystemAdmin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteLocalizationResourceCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Bulk import localization resources (UserAdmin+)
        /// </summary>
        [HttpPost("bulk-import")]
        public async Task<ActionResult<BulkImportResultDto>> BulkImport([FromBody] List<CreateLocalizationResourceDto> resources)
        {
            var result = new BulkImportResultDto { TotalProcessed = resources.Count };

            foreach (var dto in resources)
            {
                try
                {
                    var command = new CreateLocalizationResourceCommand(
                        Key: dto.Key,
                        Turkish: dto.Turkish,
                        German: dto.German,
                        English: dto.English,
                        Section: dto.Section,
                        Description: dto.Description
                    );

                    await _mediator.Send(command);
                    result.SuccessCount++;
                }
                catch (Exception ex)
                {
                    result.FailedCount++;
                    result.Errors.Add($"Key '{dto.Key}': {ex.Message}");
                }
            }

            return Ok(result);
        }
    }

    public class BulkImportResultDto
    {
        public int TotalProcessed { get; set; }
        public int SuccessCount { get; set; }
        public int FailedCount { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}
