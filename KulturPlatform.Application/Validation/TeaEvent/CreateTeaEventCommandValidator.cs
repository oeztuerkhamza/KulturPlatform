using FluentValidation;
using KulturPlatform.Application.Commands.TeaEvent;

namespace KulturPlatform.Application.Validation.TeaEvent
{
    public sealed class CreateTeaEventCommandValidator
        : AbstractValidator<CreateTeaEventCommand>
    {
        public CreateTeaEventCommandValidator()
        {
            RuleFor(x => x.TitleTr).NotEmpty();
            RuleFor(x => x.TitleDe).NotEmpty();

            RuleFor(x => x.IntroTr).NotEmpty();
            RuleFor(x => x.IntroDe).NotEmpty();

            RuleFor(x => x.HeritageTextTr).NotEmpty();
            RuleFor(x => x.HeritageTextDe).NotEmpty();

            RuleFor(x => x.ParticipationTextTr).NotEmpty();
            RuleFor(x => x.ParticipationTextDe).NotEmpty();

            RuleFor(x => x.ContactEmail)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.Location).NotEmpty();
            RuleFor(x => x.ImageUrl).NotEmpty();
        }
    }
}
