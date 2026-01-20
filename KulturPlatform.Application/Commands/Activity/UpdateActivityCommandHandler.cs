using KulturPlatform.Application.Interfaces;
using KulturPlatform.Application.Interfaces.Activity;
using KulturPlatform.Domain.Commons.ValueObjects;
using MediatR;

namespace KulturPlatform.Application.Commands.Activity
{
    public class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, Unit>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IImageProcessingService _imageProcessingService;

        public UpdateActivityCommandHandler(
            IActivityRepository activityRepository,
            IImageProcessingService imageProcessingService)
        {
            _activityRepository = activityRepository;
            _imageProcessingService = imageProcessingService;
        }

        public async Task<Unit> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
        {
            // 1️⃣ Mevcut Activity'yi repository'den çek
            var activity = await _activityRepository.GetByIdAsync(request.Id, cancellationToken);
            if (activity == null)
                throw new KeyNotFoundException($"Activity with Id {request.Id} not found.");

            // 2️⃣ VO'ları oluştur
            var activityDate = ActivityDate.FromString(request.Date);

            // 3️⃣ Process image - either URL or Base64
            Url? imageUrl = null;
            ImageData? imageData = null;

            // Validate: both cannot be provided
            if (!string.IsNullOrWhiteSpace(request.ImageUrl) && !string.IsNullOrWhiteSpace(request.ImageBase64))
                throw new ArgumentException("Cannot provide both ImageUrl and ImageBase64. Choose one.");

            if (!string.IsNullOrWhiteSpace(request.ImageBase64) && !string.IsNullOrWhiteSpace(request.ImageFileName))
            {
                // Process base64 image with compression and validation
                imageData = await _imageProcessingService.ProcessImageAsync(
                    request.ImageBase64,
                    request.ImageFileName,
                    maxWidth: 1920,
                    maxHeight: 1080,
                    quality: 85
                );
            }
            else if (!string.IsNullOrWhiteSpace(request.ImageUrl))
            {
                // This will throw if it's a data URI
                imageUrl = Url.Create(request.ImageUrl);
            }

            // 4️⃣ VideoUrl map et
            Url? videoUrl = null;
            if (!string.IsNullOrWhiteSpace(request.VideoUrl))
                videoUrl = Url.Create(request.VideoUrl);

            // 5️⃣ GalleryImages map et
            MediaGallery? galleryImages = null;
            if (request.GalleryImages != null && request.GalleryImages.Any())
            {
                var galleryImageList = new List<Domain.Commons.ValueObjects.GalleryImage>();
                
                foreach (var dto in request.GalleryImages)
                {
                    if (!string.IsNullOrWhiteSpace(dto.Base64Data) && !string.IsNullOrWhiteSpace(dto.FileName))
                    {
                        // Process base64 gallery image
                        var processedImageData = await _imageProcessingService.ProcessImageAsync(
                            dto.Base64Data,
                            dto.FileName,
                            maxWidth: 1920,
                            maxHeight: 1080,
                            quality: 85
                        );
                        galleryImageList.Add(Domain.Commons.ValueObjects.GalleryImage.FromImageData(processedImageData));
                    }
                    else if (!string.IsNullOrWhiteSpace(dto.Url))
                    {
                        // Use URL
                        galleryImageList.Add(Domain.Commons.ValueObjects.GalleryImage.FromUrl(dto.Url));
                    }
                }
                
                if (galleryImageList.Any())
                {
                    galleryImages = new MediaGallery(galleryImageList);
                }
            }

            // 6️⃣ Address VO map et
            var address = new Address(
                request.Address.Street,
                request.Address.HouseNo,
                request.Address.ZipCode,
                request.Address.City,
                request.Address.State,
                request.Address.Country
            );

            // 7️⃣ Entity'yi update et
            activity.Update(
                new Title(request.TitleTr),
                new Title(request.TitleDe),
                new Description(request.DescriptionTr),
                new Description(request.DescriptionDe),
                activityDate,
                address,
                new Category(request.Category),
                imageUrl,
                imageData,
                galleryImages,
                videoUrl,
                request.IsActive,
                detailedContentTr: request.DetailedContentTr != null ? new LocalizedContent(request.DetailedContentTr) : null,
                detailedContentDe: request.DetailedContentDe != null ? new LocalizedContent(request.DetailedContentDe) : null
            );

            // 8️⃣ Repository'de update et
            await _activityRepository.UpdateAsync(activity, cancellationToken);

            return Unit.Value;
        }
    }
}
