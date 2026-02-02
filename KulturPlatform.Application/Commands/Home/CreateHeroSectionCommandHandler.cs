using KulturPlatform.Application.Interfaces;
using KulturPlatform.Application.Interfaces.Home;
using KulturPlatform.Application.Services;
using KulturPlatform.Domain.Commons.AggregateRoot;
using KulturPlatform.Domain.Commons.ValueObjects;
using MediatR;

namespace KulturPlatform.Application.Commands.Home
{
    public class CreateHeroSectionCommandHandler : IRequestHandler<CreateHeroSectionCommand, Guid>
    {
        private readonly IHeroSectionRepository _repository;
        private readonly ImageService _imageService;

        public CreateHeroSectionCommandHandler(
            IHeroSectionRepository repository,
            ImageService imageService)
        {
            _repository = repository;
            _imageService = imageService;
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
                // Upload to storage and get URL
                var image = await _imageService.ProcessAndUploadImageAsync(
                    base64Data,
                    fileName,
                    "hero-sections",
                    maxWidth: 1920,
                    maxHeight: 1080,
                    quality: 90
                );
                return (image.ImageUrl, null); // Return URL, not database data
            }
            else if (!string.IsNullOrWhiteSpace(url))
            {
                return (Url.Create(url), null);
            }

            return (null, null);
        }
    }
}
