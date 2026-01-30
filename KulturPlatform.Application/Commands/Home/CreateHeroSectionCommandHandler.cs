using KulturPlatform.Application.Interfaces;
using KulturPlatform.Application.Interfaces.Home;
using KulturPlatform.Domain.Commons.AggregateRoot;
using KulturPlatform.Domain.Commons.ValueObjects;
using MediatR;

namespace KulturPlatform.Application.Commands.Home
{
    public class CreateHeroSectionCommandHandler : IRequestHandler<CreateHeroSectionCommand, Guid>
    {
        private readonly IHeroSectionRepository _repository;
        private readonly IImageProcessingService _imageProcessingService;

        public CreateHeroSectionCommandHandler(
            IHeroSectionRepository repository,
            IImageProcessingService imageProcessingService)
        {
            _repository = repository;
            _imageProcessingService = imageProcessingService;
        }

        public async Task<Guid> Handle(CreateHeroSectionCommand request, CancellationToken cancellationToken)
        {
            // Process background image
            var (imageUrl, imageData) = await ProcessImageAsync(
                request.BackgroundImageUrl,
                request.BackgroundImageBase64,
                request.BackgroundImageFileName
            );

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

            await _repository.AddAsync(heroSection, cancellationToken);

            return heroSection.Id;
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
