using FluentValidation;
using KulturPlatform.Application.Commands.ValueItem;

namespace KulturPlatform.Application.Validation.ValueItem
{
    public class SectionCommandValidator : AbstractValidator<SectionCommand>
    {
        public SectionCommandValidator()
        {
            RuleFor(x => x.HeadingTr)
                .NotEmpty().WithMessage("Section heading (TR) is required.")
                .MaximumLength(200);

            RuleFor(x => x.HeadingDe)
                .NotEmpty().WithMessage("Section heading (DE) is required.")
                .MaximumLength(200);

            RuleFor(x => x.BodyTr)
                .NotEmpty().WithMessage("Section body (TR) is required.")
                .MaximumLength(2000);

            RuleFor(x => x.BodyDe)
                .NotEmpty().WithMessage("Section body (DE) is required.")
                .MaximumLength(2000);

            // Items
            RuleForEach(x => x.Items).SetValidator(new SectionItemCommandValidator());
        }
    }
}
