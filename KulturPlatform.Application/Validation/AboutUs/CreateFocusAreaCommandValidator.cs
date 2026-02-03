using FluentValidation;
using KulturPlatform.Application.Commands.AboutUs;
using KulturPlatform.Application.Constants;

namespace KulturPlatform.Application.Validation.AboutUs;

/// <summary>
/// Validator for CreateFocusAreaCommand
/// </summary>
public sealed class CreateFocusAreaCommandValidator : AbstractValidator<CreateFocusAreaCommand>
{
    public CreateFocusAreaCommandValidator()
    {
        // Title validations
        RuleFor(x => x.TitleTr)
            .NotEmpty().WithMessage("Turkish title is required")
            .MaximumLength(200).WithMessage("Turkish title cannot exceed 200 characters");

        RuleFor(x => x.TitleDe)
            .NotEmpty().WithMessage("German title is required")
            .MaximumLength(200).WithMessage("German title cannot exceed 200 characters");

        // Description validations
        RuleFor(x => x.DescriptionTr)
            .NotEmpty().WithMessage("Turkish description is required")
            .MaximumLength(1000).WithMessage("Turkish description cannot exceed 1000 characters");

        RuleFor(x => x.DescriptionDe)
            .NotEmpty().WithMessage("German description is required")
            .MaximumLength(1000).WithMessage("German description cannot exceed 1000 characters");

        // Order validation
        RuleFor(x => x.Order)
            .GreaterThanOrEqualTo(0).WithMessage("Order must be a non-negative number");

        // Icon validation - Cannot provide both URL and Base64
        RuleFor(x => x)
            .Must(x => string.IsNullOrWhiteSpace(x.IconUrl) || 
                      string.IsNullOrWhiteSpace(x.IconBase64))
            .WithMessage("Cannot provide both URL and Base64 icon data. Choose one.");

        // If Base64 is provided, fileName must also be provided
        When(x => !string.IsNullOrWhiteSpace(x.IconBase64), () =>
        {
            RuleFor(x => x.IconFileName)
                .NotEmpty().WithMessage("File name is required when providing Base64 icon data");

            RuleFor(x => x.IconBase64)
                .Must(BeValidBase64!).WithMessage("Invalid Base64 icon data")
                .Must(BeValidImageSize!).WithMessage($"Icon size exceeds maximum allowed ({ImageProcessingConstants.Validation.MaxFileSizeBytes / 1024 / 1024}MB)");

            RuleFor(x => x.IconFileName)
                .Must(BeValidImageExtension!).WithMessage("Invalid icon file extension");
        });

        // URL format validation
        When(x => !string.IsNullOrWhiteSpace(x.IconUrl), () =>
        {
            RuleFor(x => x.IconUrl)
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
