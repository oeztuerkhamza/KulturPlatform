using FluentValidation;
using KulturPlatform.Application.Commands.Activity;

namespace KulturPlatform.Application.Validation.Activity
{
    public sealed class CreateActivityCommandValidator
        : AbstractValidator<CreateActivityCommand>
    {
        public CreateActivityCommandValidator()
        {
            RuleFor(x => x.TitleTr).NotEmpty().WithMessage("Turkish title is required");
            RuleFor(x => x.TitleDe).NotEmpty().WithMessage("German title is required");
            RuleFor(x => x.DescriptionTr).NotEmpty().WithMessage("Turkish description is required");
            RuleFor(x => x.DescriptionDe).NotEmpty().WithMessage("German description is required");
            RuleFor(x => x.Date).NotEmpty().WithMessage("Date is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            
            // Gallery images validation
            RuleFor(x => x.GalleryImages)
                .Must(g => g == null || g.Count <= 10)
                .WithMessage("Gallery cannot contain more than 10 images");
        }
    }
}
