using FluentValidation;
using KulturPlatform.Application.Commands.Home;
using KulturPlatform.Application.Constants;

namespace KulturPlatform.Application.Validation.Home;

/// <summary>
/// Validator for CreateHeroSectionCommand
/// Validates all input fields and enforces business rules
/// </summary>
public sealed class CreateHeroSectionCommandValidator : AbstractValidator<CreateHeroSectionCommand>
{
    public CreateHeroSectionCommandValidator()
    {
        // Title validations (Turkish)
        RuleFor(x => x.TitleTr)
            .NotEmpty().WithMessage("Turkish title is required")
            .MaximumLength(200).WithMessage("Turkish title cannot exceed 200 characters");

        // Title validations (German)
        RuleFor(x => x.TitleDe)
            .NotEmpty().WithMessage("German title is required")
            .MaximumLength(200).WithMessage("German title cannot exceed 200 characters");

        // Subtitle validations (Turkish) - Optional
        RuleFor(x => x.SubtitleTr)
            .MaximumLength(300).WithMessage("Turkish subtitle cannot exceed 300 characters")
            .When(x => !string.IsNullOrWhiteSpace(x.SubtitleTr));

        // Subtitle validations (German) - Optional
        RuleFor(x => x.SubtitleDe)
            .MaximumLength(300).WithMessage("German subtitle cannot exceed 300 characters")
            .When(x => !string.IsNullOrWhiteSpace(x.SubtitleDe));

        // Description validations (Turkish)
        RuleFor(x => x.DescriptionTr)
            .NotEmpty().WithMessage("Turkish description is required")
            .MaximumLength(1000).WithMessage("Turkish description cannot exceed 1000 characters");

        // Description validations (German)
        RuleFor(x => x.DescriptionDe)
            .NotEmpty().WithMessage("German description is required")
            .MaximumLength(1000).WithMessage("German description cannot exceed 1000 characters");

        // Primary button text validations
        RuleFor(x => x.PrimaryButtonTextTr)
            .NotEmpty().WithMessage("Turkish primary button text is required")
            .MaximumLength(50).WithMessage("Turkish primary button text cannot exceed 50 characters");

        RuleFor(x => x.PrimaryButtonTextDe)
            .NotEmpty().WithMessage("German primary button text is required")
            .MaximumLength(50).WithMessage("German primary button text cannot exceed 50 characters");

        // Secondary button text validations
        RuleFor(x => x.SecondaryButtonTextTr)
            .NotEmpty().WithMessage("Turkish secondary button text is required")
            .MaximumLength(50).WithMessage("Turkish secondary button text cannot exceed 50 characters");

        RuleFor(x => x.SecondaryButtonTextDe)
            .NotEmpty().WithMessage("German secondary button text is required")
            .MaximumLength(50).WithMessage("German secondary button text cannot exceed 50 characters");

        // Image validation - Business rule: Cannot provide both URL and Base64
        RuleFor(x => x)
            .Must(x => string.IsNullOrWhiteSpace(x.BackgroundImageUrl) || 
                      string.IsNullOrWhiteSpace(x.BackgroundImageBase64))
            .WithMessage("Cannot provide both URL and Base64 image data. Choose one.");

        // If Base64 is provided, fileName must also be provided
        When(x => !string.IsNullOrWhiteSpace(x.BackgroundImageBase64), () =>
        {
            RuleFor(x => x.BackgroundImageFileName)
                .NotEmpty().WithMessage("File name is required when providing Base64 image data");
        });

        // URL format validation
        When(x => !string.IsNullOrWhiteSpace(x.BackgroundImageUrl), () =>
        {
            RuleFor(x => x.BackgroundImageUrl)
                .Must(BeAValidUrl!).WithMessage("Invalid URL format");
        });

        // Base64 validation
        When(x => !string.IsNullOrWhiteSpace(x.BackgroundImageBase64), () =>
        {
            RuleFor(x => x.BackgroundImageBase64)
                .Must(BeValidBase64!).WithMessage("Invalid Base64 image data")
                .Must(BeValidImageSize!).WithMessage($"Image size exceeds maximum allowed ({ImageProcessingConstants.Validation.MaxFileSizeBytes / 1024 / 1024}MB)");

            RuleFor(x => x.BackgroundImageFileName)
                .Must(BeValidImageExtension!).WithMessage("Invalid image file extension. Allowed: " + 
                    string.Join(", ", ImageProcessingConstants.Validation.AllowedExtensions));
        });
    }

    private static bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
               (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

    private static bool BeValidBase64(string base64)
    {
        if (string.IsNullOrWhiteSpace(base64)) 
            return false;
        
        try
        {
            // Handle data URI format (e.g., "data:image/png;base64,...")
            var base64Data = base64;
            if (base64.Contains(','))
            {
                base64Data = base64.Split(',')[1];
            }
            
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
        if (string.IsNullOrWhiteSpace(base64)) 
            return true;
        
        try
        {
            // Handle data URI format
            var base64Data = base64;
            if (base64.Contains(','))
            {
                base64Data = base64.Split(',')[1];
            }
            
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
        if (string.IsNullOrWhiteSpace(fileName))
            return false;

        var extension = Path.GetExtension(fileName)?.ToLowerInvariant();
        return !string.IsNullOrEmpty(extension) && 
               ImageProcessingConstants.Validation.AllowedExtensions.Contains(extension);
    }
}
