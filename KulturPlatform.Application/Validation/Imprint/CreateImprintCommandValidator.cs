using FluentValidation;
using KulturPlatform.Application.Commands.Imprint;

namespace KulturPlatform.Application.Validation.Imprint
{
    public class CreateImprintCommandValidator : AbstractValidator<CreateImprintCommand>
    {
        public CreateImprintCommandValidator()
        {
            RuleFor(x => x.Dto)
                .NotNull()
                .WithMessage("Imprint DTO cannot be null.");

            // Organization info
            RuleFor(x => x.Dto.OrganizationName)
                .NotEmpty().WithMessage("Organization name is required.")
                .MaximumLength(200);

            RuleFor(x => x.Dto.OrganizationType)
                .NotEmpty().WithMessage("Organization type is required.")
                .MaximumLength(100);

            RuleFor(x => x.Dto.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(x => x.Dto.Phone)
                .NotEmpty().WithMessage("Phone is required.")
                .MaximumLength(30);

            RuleFor(x => x.Dto.President)
                .NotEmpty().WithMessage("President is required.")
                .MaximumLength(150);

            RuleFor(x => x.Dto.VicePresident)
                .MaximumLength(150);

            // Legal & purpose
            RuleFor(x => x.Dto.LegalStructureTurkish).NotEmpty().WithMessage("Legal structure (TR) is required.");
            RuleFor(x => x.Dto.LegalStructureGerman).NotEmpty().WithMessage("Legal structure (DE) is required.");
            RuleFor(x => x.Dto.PurposeTurkish).NotEmpty().WithMessage("Purpose (TR) is required.");
            RuleFor(x => x.Dto.PurposeGerman).NotEmpty().WithMessage("Purpose (DE) is required.");

            // Tax & responsibility
            RuleFor(x => x.Dto.TaxExemptionTurkish).NotEmpty().WithMessage("Tax exemption (TR) is required.");
            RuleFor(x => x.Dto.TaxExemptionGerman).NotEmpty().WithMessage("Tax exemption (DE) is required.");
            RuleFor(x => x.Dto.ContentResponsibilityTurkish).NotEmpty().WithMessage("Content responsibility (TR) is required.");
            RuleFor(x => x.Dto.ContentResponsibilityGerman).NotEmpty().WithMessage("Content responsibility (DE) is required.");
            RuleFor(x => x.Dto.LinksResponsibilityTurkish).NotEmpty().WithMessage("Links responsibility (TR) is required.");
            RuleFor(x => x.Dto.LinksResponsibilityGerman).NotEmpty().WithMessage("Links responsibility (DE) is required.");

            // Copyright
            RuleFor(x => x.Dto.CopyrightTurkish).NotEmpty().WithMessage("Copyright (TR) is required.");
            RuleFor(x => x.Dto.CopyrightGerman).NotEmpty().WithMessage("Copyright (DE) is required.");

            // Address (nested validator)
            RuleFor(x => x.Dto.Address)
                .NotNull().WithMessage("Address is required.")
                .SetValidator(new AddressDtoValidator());
        }
    }
}
