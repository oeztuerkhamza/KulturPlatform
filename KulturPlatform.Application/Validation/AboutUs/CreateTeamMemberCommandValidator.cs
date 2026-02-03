using FluentValidation;
using KulturPlatform.Application.Commands.AboutUs;
using KulturPlatform.Application.Constants;

namespace KulturPlatform.Application.Validation.AboutUs;

/// <summary>
/// Validator for CreateTeamMemberCommand
/// </summary>
public sealed class CreateTeamMemberCommandValidator : AbstractValidator<CreateTeamMemberCommand>
{
    public CreateTeamMemberCommandValidator()
    {
        // Name validation
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

        // Title validations
        RuleFor(x => x.TitleTr)
            .NotEmpty().WithMessage("Turkish title is required")
            .MaximumLength(100).WithMessage("Turkish title cannot exceed 100 characters");

        RuleFor(x => x.TitleDe)
            .NotEmpty().WithMessage("German title is required")
            .MaximumLength(100).WithMessage("German title cannot exceed 100 characters");

        // Description validations (optional)
        RuleFor(x => x.DescriptionTr)
            .MaximumLength(500).WithMessage("Turkish description cannot exceed 500 characters")
            .When(x => !string.IsNullOrWhiteSpace(x.DescriptionTr));

        RuleFor(x => x.DescriptionDe)
            .MaximumLength(500).WithMessage("German description cannot exceed 500 characters")
            .When(x => !string.IsNullOrWhiteSpace(x.DescriptionDe));

        // Order validation
        RuleFor(x => x.Order)
            .GreaterThanOrEqualTo(0).WithMessage("Order must be a non-negative number");

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
