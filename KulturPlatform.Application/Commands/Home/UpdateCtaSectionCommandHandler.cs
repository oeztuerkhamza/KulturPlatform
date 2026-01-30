using KulturPlatform.Application.Dtos.Home;
using KulturPlatform.Application.Interfaces;
using KulturPlatform.Application.Interfaces.Home;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.Home
{
    public class UpdateCtaSectionCommandHandler : IRequestHandler<UpdateCtaSectionCommand, CtaSectionDto>
    {
        private readonly ICtaSectionRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageProcessingService _imageProcessingService;

        public UpdateCtaSectionCommandHandler(
            ICtaSectionRepository repository, 
            IUnitOfWork unitOfWork,
            IImageProcessingService imageProcessingService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _imageProcessingService = imageProcessingService;
        }

        public async Task<CtaSectionDto> Handle(UpdateCtaSectionCommand request, CancellationToken cancellationToken)
        {
            var ctaSection = await _repository.GetByIdAsync(request.Id, cancellationToken)
                             ?? throw new KeyNotFoundException($"CtaSection with Id {request.Id} not found.");

            // Process background image
            var (imageUrl, imageData) = await ProcessImageAsync(
                request.BackgroundImageUrl,
                request.BackgroundImageBase64,
                request.BackgroundImageFileName
            );

            ctaSection.Update(
                new Title(request.TitleTr),
                new Title(request.TitleDe),
                new Description(request.DescriptionTr),
                new Description(request.DescriptionDe),
                imageUrl,
                imageData,
                new Title(request.PrimaryButtonTr),
                new Title(request.PrimaryButtonDe),
                new Title(request.SecondaryButtonTr),
                new Title(request.SecondaryButtonDe),
                new Title(request.DonateButtonTr),
                new Title(request.DonateButtonDe)
            );

            _repository.Update(ctaSection, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Return DTO with updated data
            return new CtaSectionDto(
                ctaSection.Id,
                ctaSection.TitleTr.Value,
                ctaSection.TitleDe.Value,
                ctaSection.DescriptionTr.Value,
                ctaSection.DescriptionDe.Value,
                ctaSection.BackgroundImageUrl?.Value,
                ctaSection.BackgroundImageData?.Base64Data,
                ctaSection.BackgroundImageData?.FileName,
                ctaSection.PrimaryButtonTr.Value,
                ctaSection.PrimaryButtonDe.Value,
                ctaSection.SecondaryButtonTr.Value,
                ctaSection.SecondaryButtonDe.Value,
                ctaSection.DonateButtonTr.Value,
                ctaSection.DonateButtonDe.Value
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
