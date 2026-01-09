using FluentValidation;
using KulturPlatform.Application.Dtos.LocalizationDto;
using System.Linq.Expressions;

namespace KulturPlatform.Application.Validation.Activity
{
    public static class ActivityValidationRules
    {
        public static void ApplyActivityRules<T>(
            this AbstractValidator<T> validator,
            Expression<Func<T, string>> titleTr,
            Expression<Func<T, string>> titleDe,
            Expression<Func<T, string>> descriptionTr,
            Expression<Func<T, string>> descriptionDe,
            Expression<Func<T, string>> date,
            Expression<Func<T, AddressDto>> address,
            Expression<Func<T, string>> category,
            Expression<Func<T, string?>> imageUrl,
            Expression<Func<T, string?>> videoUrl,
            Expression<Func<T, List<string>?>> galleryImages,
            Expression<Func<T, string?>> detailedContentTr,
            Expression<Func<T, string?>> detailedContentDe
        )
        {
            validator.RuleFor(titleTr).NotEmpty().MaximumLength(200);
            validator.RuleFor(titleDe).NotEmpty().MaximumLength(200);

            validator.RuleFor(descriptionTr).NotEmpty().MaximumLength(1000);
            validator.RuleFor(descriptionDe).NotEmpty().MaximumLength(1000);

            validator.RuleFor(date)
                .NotEmpty()
                .Must(d => DateTime.TryParse(d, out _))
                .WithMessage("Invalid date format");

            validator.RuleFor(category).NotEmpty().MaximumLength(50);

            validator.RuleFor(imageUrl)
                .Must(BeValidUrl)
                .When(x => !string.IsNullOrWhiteSpace(imageUrl.Compile()(x)));

            validator.RuleFor(videoUrl)
                .Must(BeValidUrl)
                .When(x => !string.IsNullOrWhiteSpace(videoUrl.Compile()(x)));

            validator.RuleFor(galleryImages)
    .Must(list => list == null || list.All(BeValidUrl))
    .WithMessage("One or more gallery image URLs are invalid.");


            validator.RuleFor(detailedContentTr)
                .MaximumLength(4000)
                .When(x => !string.IsNullOrWhiteSpace(detailedContentTr.Compile()(x)));

            validator.RuleFor(detailedContentDe)
                .MaximumLength(4000)
                .When(x => !string.IsNullOrWhiteSpace(detailedContentDe.Compile()(x)));

            validator.RuleFor(address).NotNull();
            validator.RuleFor(address).ChildRules(a =>
            {
                a.RuleFor(x => x.Street).NotEmpty();
                a.RuleFor(x => x.HouseNo).NotEmpty();
                a.RuleFor(x => x.City).NotEmpty();
                a.RuleFor(x => x.Country).NotEmpty();
            });
        }

        private static bool BeValidUrl(string? url)
            => Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}
