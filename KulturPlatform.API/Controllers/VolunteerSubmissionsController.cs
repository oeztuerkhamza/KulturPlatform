using KulturPlatform.Application.Commands.VolunteerSubmission;
using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Interfaces.Localization;
using KulturPlatform.Application.Queries.VolunteerSubmission;
using KulturPlatform.Application.Resources;
using KulturPlatform.Domain.Commons.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KulturPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VolunteerSubmissionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILocalizationService _localization;

        public VolunteerSubmissionsController(IMediator mediator, ILocalizationService localization)
        {
            _mediator = mediator;
            _localization = localization;
        }

        /// <summary>
        /// Get volunteer page content for frontend (Public) with localization support
        /// </summary>
        /// <param name="lang">Language code: tr, de, en (default: tr)</param>
        [HttpGet("page")]
        [AllowAnonymous]
        public ActionResult<VolunteerPageDto> GetPageContent([FromQuery] string lang = "tr")
        {
            var pageContent = new VolunteerPageDto
            {
                Hero = new HeroSectionDto
                {
                    Title = _localization.GetText(ResourceKeys.Volunteer.HeroTitle, lang),
                    Subtitle = _localization.GetText(ResourceKeys.Volunteer.HeroSubtitle, lang),
                    Description = _localization.GetText(ResourceKeys.Volunteer.HeroDescription, lang),
                    BackgroundImage = "/images/volunteer-hero.jpg",
                    CtaButtonText = _localization.GetText(ResourceKeys.Volunteer.HeroCtaButton, lang)
                },
                Benefits = new List<BenefitDto>
                {
                    new BenefitDto
                    {
                        Icon = "??",
                        Title = _localization.GetText(ResourceKeys.Volunteer.BenefitCommunityTitle, lang),
                        Description = _localization.GetText(ResourceKeys.Volunteer.BenefitCommunityDescription, lang)
                    },
                    new BenefitDto
                    {
                        Icon = "??",
                        Title = _localization.GetText(ResourceKeys.Volunteer.BenefitSkillsTitle, lang),
                        Description = _localization.GetText(ResourceKeys.Volunteer.BenefitSkillsDescription, lang)
                    },
                    new BenefitDto
                    {
                        Icon = "??",
                        Title = _localization.GetText(ResourceKeys.Volunteer.BenefitNetworkTitle, lang),
                        Description = _localization.GetText(ResourceKeys.Volunteer.BenefitNetworkDescription, lang)
                    },
                    new BenefitDto
                    {
                        Icon = "??",
                        Title = _localization.GetText(ResourceKeys.Volunteer.BenefitCertificateTitle, lang),
                        Description = _localization.GetText(ResourceKeys.Volunteer.BenefitCertificateDescription, lang)
                    }
                },
                Form = new VolunteerFormDto
                {
                    Title = _localization.GetText(ResourceKeys.Volunteer.FormTitle, lang),
                    Description = _localization.GetText(ResourceKeys.Volunteer.FormDescription, lang),
                    Fields = new List<FormFieldDto>
                    {
                        new FormFieldDto
                        {
                            Name = "fullName",
                            Label = _localization.GetText(ResourceKeys.Volunteer.FormNameLabel, lang),
                            Type = "text",
                            Placeholder = _localization.GetText(ResourceKeys.Volunteer.FormNameLabel, lang),
                            Required = true,
                            ValidationMessage = _localization.GetText(ResourceKeys.Volunteer.FormNameLabel, lang)
                        },
                        new FormFieldDto
                        {
                            Name = "email",
                            Label = _localization.GetText(ResourceKeys.Volunteer.FormEmailLabel, lang),
                            Type = "email",
                            Placeholder = _localization.GetText(ResourceKeys.Volunteer.FormEmailLabel, lang),
                            Required = true,
                            ValidationMessage = _localization.GetText(ResourceKeys.Volunteer.FormEmailLabel, lang)
                        },
                        new FormFieldDto
                        {
                            Name = "phone",
                            Label = _localization.GetText(ResourceKeys.Volunteer.FormPhoneLabel, lang),
                            Type = "tel",
                            Placeholder = "+49 123 456 7890",
                            Required = true,
                            ValidationMessage = _localization.GetText(ResourceKeys.Volunteer.FormPhoneLabel, lang)
                        },
                        new FormFieldDto
                        {
                            Name = "message",
                            Label = _localization.GetText(ResourceKeys.Volunteer.FormMessageLabel, lang),
                            Type = "textarea",
                            Placeholder = _localization.GetText(ResourceKeys.Volunteer.FormMessageLabel, lang),
                            Required = true,
                            ValidationMessage = _localization.GetText(ResourceKeys.Volunteer.FormMessageLabel, lang)
                        }
                    },
                    SubmitButtonText = _localization.GetText(ResourceKeys.Volunteer.FormSubmitButton, lang),
                    SuccessMessage = _localization.GetText(ResourceKeys.Volunteer.FormSuccessMessage, lang),
                    ErrorMessage = _localization.GetText(ResourceKeys.Volunteer.FormErrorMessage, lang)
                },
                Testimonials = new TestimonialsDto
                {
                    Title = "Gönüllülerimizin Deneyimleri",
                    Items = new List<TestimonialDto>
                    {
                        new TestimonialDto
                        {
                            Name = "Ay?e Y?lmaz",
                            Quote = "KPF'de gönüllü olmak bana çok ?ey katt?. Hem yeni insanlarla tan??t?m hem de topluma katk?da bulunman?n mutlulu?unu ya?ad?m.",
                            Image = "/images/testimonials/ayse.jpg",
                            Role = "Etkinlik Koordinatörü"
                        },
                        new TestimonialDto
                        {
                            Name = "Mehmet Demir",
                            Quote = "Gönüllü olarak ba?lad?m, ?imdi ekibin ayr?lmaz bir parças?y?m. Kesinlikle tavsiye ederim!",
                            Image = "/images/testimonials/mehmet.jpg",
                            Role = "Sosyal Medya Yöneticisi"
                        }
                    }
                }
            };

            return Ok(pageContent);
        }

        // GET - UserAdmin+ (View submissions)
        [HttpGet]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<ActionResult<IEnumerable<VolunteerSubmissionDto>>> GetAll()
        {
            var query = new GetAllVolunteerSubmissionsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<ActionResult<VolunteerSubmissionDto>> GetById(Guid id)
        {
            var query = new GetVolunteerSubmissionByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST - Public (Anyone can submit)
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateVolunteerSubmissionCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        // DELETE - SystemAdmin only
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.RequireSystemAdmin)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteVolunteerSubmissionCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
