using FluentValidation;
using KulturPlatform.Application.Commands.ValueItem;

namespace KulturPlatform.Application.Validation.ValueItem
{
    public class SectionItemCommandValidator : AbstractValidator<SectionItemCommand>
    {
        public SectionItemCommandValidator()
        {
            RuleFor(x => x.TitleTr)
                .NotEmpty().WithMessage("Section item title (TR) is required.")
                .MaximumLength(200);

            RuleFor(x => x.TitleDe)
                .NotEmpty().WithMessage("Section item title (DE) is required.")
                .MaximumLength(200);

            RuleFor(x => x.Icon)
                .MaximumLength(100).WithMessage("Icon max length is 100 characters.");
        }
    }
}
