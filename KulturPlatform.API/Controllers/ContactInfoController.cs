using KulturPlatform.Application.Commands.ContactInfo;
using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Interfaces.Localization;
using KulturPlatform.Application.Queries.ContactInfo;
using KulturPlatform.Application.Resources;
using KulturPlatform.Domain.Commons.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KulturPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactInfoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILocalizationService _localization;

        public ContactInfoController(IMediator mediator, ILocalizationService localization)
        {
            _mediator = mediator;
            _localization = localization;
        }

        /// <summary>
        /// Get contact information (Public) - Database-driven
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ContactInfoDto>> Get()
        {
            var query = new GetContactInfoQuery();
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound("Contact information not found.");

            return Ok(result);
        }

        /// <summary>
        /// Get contact page content for frontend (Public) with localization
        /// </summary>
        [HttpGet("page")]
        [AllowAnonymous]
        public async Task<ActionResult<ContactPageDto>> GetPageContent([FromQuery] string lang = "tr")
        {
            var contactInfo = await _mediator.Send(new GetContactInfoQuery());

            if (contactInfo == null)
                return NotFound("Contact information not found.");

            var pageContent = new ContactPageDto
            {
                ContactInfo = contactInfo,
                Form = new ContactFormDto
                {
                    Title = _localization.GetText(ResourceKeys.Contact.FormTitle, lang),
                    Description = _localization.GetText(ResourceKeys.Contact.FormDescription, lang),
                    Fields = new List<FormFieldDto>
                    {
                        new FormFieldDto
                        {
                            Name = "name",
                            Label = _localization.GetText(ResourceKeys.Contact.FormNameLabel, lang),
                            Type = "text",
                            Placeholder = _localization.GetText(ResourceKeys.Contact.FormNamePlaceholder, lang),
                            Required = true,
                            ValidationMessage = _localization.GetText(ResourceKeys.Contact.ValidationNameRequired, lang)
                        },
                        new FormFieldDto
                        {
                            Name = "email",
                            Label = _localization.GetText(ResourceKeys.Contact.FormEmailLabel, lang),
                            Type = "email",
                            Placeholder = _localization.GetText(ResourceKeys.Contact.FormEmailPlaceholder, lang),
                            Required = true,
                            ValidationMessage = _localization.GetText(ResourceKeys.Contact.ValidationEmailInvalid, lang)
                        },
                        new FormFieldDto
                        {
                            Name = "phone",
                            Label = _localization.GetText(ResourceKeys.Contact.FormPhoneLabel, lang),
                            Type = "tel",
                            Placeholder = _localization.GetText(ResourceKeys.Contact.FormPhonePlaceholder, lang),
                            Required = false
                        },
                        new FormFieldDto
                        {
                            Name = "subject",
                            Label = _localization.GetText(ResourceKeys.Contact.FormSubjectLabel, lang),
                            Type = "text",
                            Placeholder = _localization.GetText(ResourceKeys.Contact.FormSubjectPlaceholder, lang),
                            Required = true,
                            ValidationMessage = _localization.GetText(ResourceKeys.Contact.ValidationSubjectRequired, lang)
                        },
                        new FormFieldDto
                        {
                            Name = "message",
                            Label = _localization.GetText(ResourceKeys.Contact.FormMessageLabel, lang),
                            Type = "textarea",
                            Placeholder = _localization.GetText(ResourceKeys.Contact.FormMessagePlaceholder, lang),
                            Required = true,
                            ValidationMessage = _localization.GetText(ResourceKeys.Contact.ValidationMessageRequired, lang)
                        }
                    },
                    SubmitButtonText = _localization.GetText(ResourceKeys.Contact.FormSubmitButton, lang),
                    SuccessMessage = _localization.GetText(ResourceKeys.Contact.FormSuccessMessage, lang),
                    ErrorMessage = _localization.GetText(ResourceKeys.Contact.FormErrorMessage, lang)
                },
                Map = new MapDto
                {
                    Title = _localization.GetText(ResourceKeys.Contact.MapTitle, lang),
                    Latitude = 50.1109,
                    Longitude = 8.6821,
                    EmbedUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2558.0!2d8.6821!3d50.1109!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x0%3A0x0!2zNTDCsDA2JzM5LjIiTiA4wrA0MCc1NS42IkU!5e0!3m2!1sen!2sde!4v1234567890123!5m2!1sen!2sde"
                }
            };

            return Ok(pageContent);
        }

        /// <summary>
        /// Create contact information (SystemAdmin only) - First time setup
        /// </summary>
        [HttpPost]
        [Authorize(Policy = Policies.RequireSystemAdmin)]
        public async Task<ActionResult<Guid>> Create([FromBody] SaveContactInfoDto dto)
        {
            // DTO’yu command içine geçiriyoruz
            var command = new CreateContactInfoCommand(dto);

            // Mediator ile handler çalıştırıyoruz
            var id = await _mediator.Send(command);

            // Basitçe CreatedAtAction yerine Ok kullanabilirsin veya bir GetById endpoint ekle
            return Ok(id);
        }

        /// <summary>
        /// Update contact information (UserAdmin+)
        /// </summary>
        [HttpPut]
        [Authorize(Policy = Policies.RequireUserAdmin)]
        public async Task<IActionResult> Update([FromBody] UpdateContactInfoCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Delete contact information (SystemAdmin only)
        /// </summary>
        [HttpDelete]
        [Authorize(Policy = Policies.RequireSystemAdmin)]
        public async Task<IActionResult> Delete([FromBody] DeleteContactInfoCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Get supported languages (Public)
        /// </summary>
        [HttpGet("languages")]
        [AllowAnonymous]
        public ActionResult<List<string>> GetSupportedLanguages()
        {
            return Ok(_localization.GetSupportedLanguages());
        }

        /// <summary>
        /// Send contact form message (Public)
        /// </summary>
        [HttpPost("message")]
        [AllowAnonymous]
        public IActionResult SendMessage([FromBody] ContactMessageDto dto)
        {
            // TODO: Implement email service
            // TODO: Save to ContactMessages table (new entity)
            return Ok(new
            {
                success = true,
                message = "Mesajınız başarıyla gönderildi!"
            });
        }
    }

    public class ContactMessageDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
