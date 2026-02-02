using KulturPlatform.Application.Interfaces;
using KulturPlatform.Application.Services;
using PartnerRepo = KulturPlatform.Application.Interfaces.Partner.IPartnerRepository;
using KulturPlatform.Domain.Commons.ValueObjects;
using MediatR;

namespace KulturPlatform.Application.Commands.Partner
{
    public class UpdatePartnerCommandHandler : IRequestHandler<UpdatePartnerCommand, Unit>
    {
        private readonly PartnerRepo _repository;
        private readonly ImageService _imageService;

        public UpdatePartnerCommandHandler(
            PartnerRepo repository,
            ImageService imageService)
        {
            _repository = repository;
            _imageService = imageService;
        }

        public async Task<Unit> Handle(UpdatePartnerCommand request, CancellationToken cancellationToken)
        {
            var partner = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (partner == null)
                throw new KeyNotFoundException($"Partner with Id {request.Id} not found.");

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

            partner.Update(name, descriptionTr, descriptionDe, displayOrder, logoUrl, logoData, websiteUrl);

            _repository.Update(partner, cancellationToken);

            return Unit.Value;
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
                    "partners",
                    maxWidth: 800,
                    maxHeight: 800,
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
