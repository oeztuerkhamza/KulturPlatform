using KulturPlatform.Application.Constants;
using KulturPlatform.Application.Interfaces;
using KulturPlatform.Application.Interfaces.Home;
using KulturPlatform.Domain.Commons.AggregateRoot;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KulturPlatform.Application.Commands.Home
{
    public class CreateHeroSectionCommandHandler : IRequestHandler<CreateHeroSectionCommand, Guid>
    {
        private readonly IHeroSectionRepository _repository;
        private readonly IImageService _imageService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateHeroSectionCommandHandler> _logger;

        public CreateHeroSectionCommandHandler(
            IHeroSectionRepository repository,
            IImageService imageService,
            IUnitOfWork unitOfWork,
            ILogger<CreateHeroSectionCommandHandler> logger)
        {
            _repository = repository;
            _imageService = imageService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateHeroSectionCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating hero section with title (TR): {TitleTr}", request.TitleTr);
            
            Url? uploadedImageUrl = null;

            try
            {
                // Process background image
                var (imageUrl, imageData) = await ProcessImageAsync(
                    request.BackgroundImageUrl,
                    request.BackgroundImageBase64,
                    request.BackgroundImageFileName,
                    cancellationToken
                );
                uploadedImageUrl = imageUrl;

                // Create domain entity
                var heroSection = HeroSection.Create(
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

                // Persist to repository
                await _repository.AddAsync(heroSection, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Hero section created successfully with ID: {HeroSectionId}", heroSection.Id);

                return heroSection.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create hero section");

                // Compensation: Clean up uploaded image if DB save failed
                if (uploadedImageUrl != null && !string.IsNullOrWhiteSpace(request.BackgroundImageBase64))
                {
                    try
                    {
                        await _imageService.DeleteImageByUrlAsync(uploadedImageUrl.Value, cancellationToken);
                        _logger.LogWarning("Cleaned up orphaned image after failed hero section creation: {ImageUrl}", uploadedImageUrl.Value);
                    }
                    catch (Exception cleanupEx)
                    {
                        _logger.LogError(cleanupEx, "Failed to cleanup orphaned image: {ImageUrl}", uploadedImageUrl.Value);
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
            // Note: Validation is now handled by CreateHeroSectionCommandValidator
            // This method focuses only on image processing logic

            if (!string.IsNullOrWhiteSpace(base64Data) && !string.IsNullOrWhiteSpace(fileName))
            {
                _logger.LogDebug("Processing Base64 image for hero section: {FileName}", fileName);
                
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
                _logger.LogDebug("Using provided URL for hero section image: {Url}", url);
                return (Url.Create(url), null);
            }

            return (null, null);
        }
    }
}
