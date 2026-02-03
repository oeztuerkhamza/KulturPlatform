using FluentValidation;
using KulturPlatform.Application.Commands.TeaEvent;
using KulturPlatform.Application.Constants;

namespace KulturPlatform.Application.Validation.TeaEvent
{
    public sealed class CreateTeaEventCommandValidator
        : AbstractValidator<CreateTeaEventCommand>
    {
        public CreateTeaEventCommandValidator()
        {
            // Title validations
            RuleFor(x => x.TitleTr)
                .NotEmpty().WithMessage("Turkish title is required")
                .MaximumLength(200).WithMessage("Turkish title cannot exceed 200 characters");

            RuleFor(x => x.TitleDe)
                .NotEmpty().WithMessage("German title is required")
                .MaximumLength(200).WithMessage("German title cannot exceed 200 characters");

            // Intro validations
            RuleFor(x => x.IntroTr)
                .NotEmpty().WithMessage("Turkish intro is required")
                .MaximumLength(1000).WithMessage("Turkish intro cannot exceed 1000 characters");

            RuleFor(x => x.IntroDe)
                .NotEmpty().WithMessage("German intro is required")
                .MaximumLength(1000).WithMessage("German intro cannot exceed 1000 characters");

            // Heritage text validations
            RuleFor(x => x.HeritageTextTr)
                .NotEmpty().WithMessage("Turkish heritage text is required");

            RuleFor(x => x.HeritageTextDe)
                .NotEmpty().WithMessage("German heritage text is required");

            // Participation text validations
            RuleFor(x => x.ParticipationTextTr)
                .NotEmpty().WithMessage("Turkish participation text is required");

            RuleFor(x => x.ParticipationTextDe)
                .NotEmpty().WithMessage("German participation text is required");

            // Contact email validation
            RuleFor(x => x.ContactEmail)
                .NotEmpty().WithMessage("Contact email is required")
                .EmailAddress().WithMessage("Invalid email format");

            // Date and location validations
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Date is required");

            RuleFor(x => x.Time)
                .NotEmpty().WithMessage("Time is required");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Location is required")
                .MaximumLength(500).WithMessage("Location cannot exceed 500 characters");

            // Image validation - Cannot provide both URL and Base64
            RuleFor(x => x)
                .Must(x => string.IsNullOrWhiteSpace(x.ImageUrl) || 
                          string.IsNullOrWhiteSpace(x.ImageBase64))
                .WithMessage("Cannot provide both URL and Base64 image data. Choose one.");

            // If Base64 is provided, fileName must also be provided
            When(x => !string.IsNullOrWhiteSpace(x.ImageBase64), () =>
            {
                RuleFor(x => x.ImageFileName)
                    .NotEmpty().WithMessage("File name is required when providing Base64 image data");

                RuleFor(x => x.ImageBase64)
                    .Must(BeValidBase64!).WithMessage("Invalid Base64 image data")
                    .Must(BeValidImageSize!).WithMessage($"Image size exceeds maximum allowed ({ImageProcessingConstants.Validation.MaxFileSizeBytes / 1024 / 1024}MB)");

                RuleFor(x => x.ImageFileName)
                    .Must(BeValidImageExtension!).WithMessage("Invalid image file extension");
            });

            // URL format validation
            When(x => !string.IsNullOrWhiteSpace(x.ImageUrl), () =>
            {
                RuleFor(x => x.ImageUrl)
                    .Must(BeAValidUrl!).WithMessage("Invalid URL format");
            });
        }

        private static bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
                   (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        private static bool BeValidBase64(string base64)
        {
            if (string.IsNullOrWhiteSpace(base64)) return false;
            
            try
            {
                var base64Data = base64.Contains(',') ? base64.Split(',')[1] : base64;
                Convert.FromBase64String(base64Data);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool BeValidImageSize(string base64)
        {
            if (string.IsNullOrWhiteSpace(base64)) return true;
            
            try
            {
                var base64Data = base64.Contains(',') ? base64.Split(',')[1] : base64;
                var bytes = Convert.FromBase64String(base64Data);
                return bytes.Length <= ImageProcessingConstants.Validation.MaxFileSizeBytes;
            }
            catch
            {
                return false;
            }
        }

        private static bool BeValidImageExtension(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return false;
            var extension = Path.GetExtension(fileName)?.ToLowerInvariant();
            return !string.IsNullOrEmpty(extension) && 
                   ImageProcessingConstants.Validation.AllowedExtensions.Contains(extension);
        }
    }
}
