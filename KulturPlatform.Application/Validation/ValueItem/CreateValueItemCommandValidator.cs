using FluentValidation;
using KulturPlatform.Application.Commands.ValueItem;

namespace KulturPlatform.Application.Validation.ValueItem
{
    public class CreateValueItemCommandValidator : AbstractValidator<CreateValueItemCommand>
    {
        public CreateValueItemCommandValidator()
        {
            RuleFor(x => x.TitleTr)
                .NotEmpty().WithMessage("Title (TR) is required.")
                .MaximumLength(200);

            RuleFor(x => x.TitleDe)
                .NotEmpty().WithMessage("Title (DE) is required.")
                .MaximumLength(200);

            RuleFor(x => x.IntroTr)
                .NotEmpty().WithMessage("Description (TR) is required.")
                .MaximumLength(1000);

            RuleFor(x => x.IntroDe)
                .NotEmpty().WithMessage("Description (DE) is required.")
                .MaximumLength(1000);

            // Sections
            RuleFor(x => x.NameAndPurpose).SetValidator(new SectionCommandValidator());
            RuleFor(x => x.Why).SetValidator(new SectionCommandValidator());
            RuleFor(x => x.Who).SetValidator(new SectionCommandValidator());
            RuleFor(x => x.How).SetValidator(new SectionCommandValidator());
        }
    }
}