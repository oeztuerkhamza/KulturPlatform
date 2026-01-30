using KulturPlatform.Application.Dtos.Home;
using KulturPlatform.Application.Interfaces;
using KulturPlatform.Application.Interfaces.Home;
using KulturPlatform.Domain.Commons.ValueObjects;
using MediatR;

namespace KulturPlatform.Application.Commands.Home
{
    public class UpdateHeroSectionCommandHandler : IRequestHandler<UpdateHeroSectionCommand, HeroSectionDto>
    {
        private readonly IHeroSectionRepository _repository;
        private readonly IImageProcessingService _imageProcessingService;

        public UpdateHeroSectionCommandHandler(
            IHeroSectionRepository repository,
            IImageProcessingService imageProcessingService)
        {
            _repository = repository;
            _imageProcessingService = imageProcessingService;
        }

        public async Task<HeroSectionDto> Handle(UpdateHeroSectionCommand request, CancellationToken cancellationToken)
        {
            var heroSection = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (heroSection == null)
                throw new KeyNotFoundException($"HeroSection with Id {request.Id} not found.");

            // Process background image
            var (imageUrl, imageData) = await ProcessImageAsync(
                request.BackgroundImageUrl,
                request.BackgroundImageBase64,
                request.BackgroundImageFileName
            );

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

            _repository.Update(heroSection, cancellationToken);

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

        private async Task<(Url? imageUrl, ImageData? imageData)> ProcessImageAsync(
            string? url,
            string? base64Data,
            string? fileName)
        {
            // Both cannot be provided
            if (!string.IsNullOrWhiteSpace(url) && !string.IsNullOrWhiteSpace(base64Data))
                throw new ArgumentException("Cannot provide both URL and Base64 image. Choose one.");

            if (!string.IsNullOrWhiteSpace(base64Data) && !string.IsNullOrWhiteSpace(fileName))
            {
                var processedImageData = await _imageProcessingService.ProcessImageAsync(
                    base64Data,
                    fileName,
                    maxWidth: 1920,
                    maxHeight: 1080,
                    quality: 85
                );
                return (null, processedImageData);
            }
            else if (!string.IsNullOrWhiteSpace(url))
            {
                return (Url.Create(url), null);
            }

            return (null, null);
        }
    }
}
