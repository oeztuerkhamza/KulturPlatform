using KulturPlatform.Application.Constants;
using KulturPlatform.Application.Dtos.Home;
using KulturPlatform.Application.Interfaces;
using KulturPlatform.Application.Interfaces.Home;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KulturPlatform.Application.Commands.Home
{
    public class UpdateHeroSectionCommandHandler : IRequestHandler<UpdateHeroSectionCommand, HeroSectionDto>
    {
        private readonly IHeroSectionRepository _repository;
        private readonly IImageService _imageService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateHeroSectionCommandHandler> _logger;

        public UpdateHeroSectionCommandHandler(
            IHeroSectionRepository repository,
            IImageService imageService,
            IUnitOfWork unitOfWork,
            ILogger<UpdateHeroSectionCommandHandler> logger)
        {
            _repository = repository;
            _imageService = imageService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<HeroSectionDto> Handle(UpdateHeroSectionCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating hero section with ID: {HeroSectionId}", request.Id);

            var heroSection = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (heroSection == null)
            {
                _logger.LogWarning("Hero section not found with ID: {HeroSectionId}", request.Id);
                throw new KeyNotFoundException($"HeroSection with Id {request.Id} not found.");
            }

            Url? newUploadedImageUrl = null;
            Url? oldImageUrl = heroSection.BackgroundImageUrl; // Save old URL for cleanup

            try
            {
                // Process background image
                var (imageUrl, imageData) = await ProcessImageAsync(
                    request.BackgroundImageUrl,
                    request.BackgroundImageBase64,
                    request.BackgroundImageFileName,
                    cancellationToken
                );

                // Track newly uploaded image for potential cleanup
                if (imageUrl != null && !string.IsNullOrWhiteSpace(request.BackgroundImageBase64))
                {
                    newUploadedImageUrl = imageUrl;
                }

                // Update entity
                heroSection.Update(
                    new Title(request.TitleTr),
                    new Title(request.TitleDe),
                    new Title(request.SubtitleTr),
                    new Title(request.SubtitleDe),
                    new Description(request.DescriptionTr),
                    new Description(request.DescriptionDe),
                    imageUrl,
                    imageData,
                    new Title(request.PrimaryButtonTextTr),
                    new Title(request.PrimaryButtonTextDe),
                    new Title(request.SecondaryButtonTextTr),
                    new Title(request.SecondaryButtonTextDe)
                );

                // Mark as modified
                _repository.Update(heroSection, cancellationToken);

                // ? CRITICAL: Commit changes to database
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Hero section updated successfully: {HeroSectionId}", heroSection.Id);

                // Delete old image after successful update (if new image was uploaded)
                if (oldImageUrl != null && newUploadedImageUrl != null && oldImageUrl.Value != newUploadedImageUrl.Value)
                {
                    try
                    {
                        await _imageService.DeleteImageByUrlAsync(oldImageUrl.Value, cancellationToken);
                        _logger.LogInformation("Old image deleted: {OldImageUrl}", oldImageUrl.Value);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Failed to delete old image: {OldImageUrl}", oldImageUrl.Value);
                        // Don't fail the update if old image deletion fails
                    }
                }

                // Return DTO with updated data
                return new HeroSectionDto(
                    heroSection.Id,
                    heroSection.TitleTr.Value,
                    heroSection.TitleDe.Value,
                    heroSection.SubtitleTr.Value,
                    heroSection.SubtitleDe.Value,
                    heroSection.DescriptionTr.Value,
                    heroSection.DescriptionDe.Value,
                    heroSection.BackgroundImageUrl?.Value,
                    heroSection.BackgroundImageData?.Base64Data,
                    heroSection.BackgroundImageData?.FileName,
                    heroSection.PrimaryButtonTextTr.Value,
                    heroSection.PrimaryButtonTextDe.Value,
                    heroSection.SecondaryButtonTextTr.Value,
                    heroSection.SecondaryButtonTextDe.Value
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update hero section: {HeroSectionId}", request.Id);

                // Compensation: Clean up newly uploaded image if DB update failed
                if (newUploadedImageUrl != null)
                {
                    try
                    {
                        await _imageService.DeleteImageByUrlAsync(newUploadedImageUrl.Value, cancellationToken);
                        _logger.LogWarning("Cleaned up orphaned image after failed update: {ImageUrl}", newUploadedImageUrl.Value);
                    }
                    catch (Exception cleanupEx)
                    {
                        _logger.LogError(cleanupEx, "Failed to cleanup orphaned image: {ImageUrl}", newUploadedImageUrl.Value);
                    }
                }

                throw;
            }
        }

        private async Task<(Url? imageUrl, ImageData? imageData)> ProcessImageAsync(
            string? url,
            string? base64Data,
            string? fileName,
            CancellationToken cancellationToken)
        {
            // Note: Validation is handled by UpdateHeroSectionCommandValidator

            if (!string.IsNullOrWhiteSpace(base64Data) && !string.IsNullOrWhiteSpace(fileName))
            {
                _logger.LogDebug("Processing Base64 image for hero section update: {FileName}", fileName);

                // Upload to storage and get URL using constants
                var image = await _imageService.ProcessAndUploadImageAsync(
                    base64Data,
                    fileName,
                    ImageProcessingConstants.HeroSection.ContainerPath,
                    maxWidth: ImageProcessingConstants.HeroSection.MaxWidth,
                    maxHeight: ImageProcessingConstants.HeroSection.MaxHeight,
                    quality: ImageProcessingConstants.HeroSection.Quality,
                    cancellationToken
                );

                return (image.ImageUrl, null); // Return URL, not database data
            }
            else if (!string.IsNullOrWhiteSpace(url))
            {
                _logger.LogDebug("Using provided URL for hero section update: {Url}", url);
                return (Url.Create(url), null);
            }

            // If neither provided, keep existing image (don't change it)
            return (null, null);
        }
    }
}
