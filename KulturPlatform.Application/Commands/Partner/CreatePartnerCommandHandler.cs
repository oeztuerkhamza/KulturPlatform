using KulturPlatform.Application.Interfaces;
using PartnerRepo = KulturPlatform.Application.Interfaces.Partner.IPartnerRepository;
using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.ValueObjects;
using MediatR;

namespace KulturPlatform.Application.Commands.Partner
{
    public class CreatePartnerCommandHandler : IRequestHandler<CreatePartnerCommand, Guid>
    {
        private readonly PartnerRepo _repository;
        private readonly IImageProcessingService _imageProcessingService;

        public CreatePartnerCommandHandler(
            PartnerRepo repository,
            IImageProcessingService imageProcessingService)
        {
            _repository = repository;
            _imageProcessingService = imageProcessingService;
        }

        public async Task<Guid> Handle(CreatePartnerCommand request, CancellationToken cancellationToken)
        {
            var name = new PartnerName(request.Name);
            var descriptionTr = new Description(request.DescriptionTr);
            var descriptionDe = new Description(request.DescriptionDe);
            var displayOrder = new DisplayOrder(request.DisplayOrder);

            // Process logo image
            var (logoUrl, logoData) = await ProcessImageAsync(
                request.LogoUrl,
                request.LogoBase64,
                request.LogoFileName
            );

            // Website URL
            Url? websiteUrl = !string.IsNullOrWhiteSpace(request.WebsiteUrl)
                ? Url.Create(request.WebsiteUrl)
                : null;

            var partner = Domain.Commons.Aggregates.Partner.CreateNew(
                name,
                descriptionTr,
                descriptionDe,
                displayOrder,
                logoUrl,
                logoData,
                websiteUrl
            );

            await _repository.AddAsync(partner, cancellationToken);

            return partner.Id;
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
                    maxWidth: 800,
                    maxHeight: 800,
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
