using FluentValidation;
using KulturPlatform.Application.Commands.AboutUs;
using KulturPlatform.Application.Dtos.AboutUs;

namespace KulturPlatform.Application.Validation.AboutUs
{
    public class CreateOrUpdateAboutUsValidator : AbstractValidator<CreateOrUpdateAboutUsCommand>
    {
        public CreateOrUpdateAboutUsValidator()
        {
            RuleFor(x => x.Key).NotEmpty().WithMessage("Key bo? olamaz.");

            RuleFor(x => x.Dto).NotNull();
            When(x => x.Dto != null, () =>
            {
                RuleForEach(x => x.Dto.Sections).ChildRules(s =>
                {
                    s.RuleFor(sec => sec.Key).NotEmpty().WithMessage("Section key bo? olamaz.");
                    s.RuleFor(sec => sec.Content).NotNull();
                    s.RuleFor(sec => sec.Content.BodyTurkish).NotNull();
                });
            });
        }
    }
}
