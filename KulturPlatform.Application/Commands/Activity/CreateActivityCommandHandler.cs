using KulturPlatform.Application.Interfaces;
using KulturPlatform.Application.Interfaces.Activity;
using KulturPlatform.Domain.Commons.ValueObjects;
using MediatR;

namespace KulturPlatform.Application.Commands.Activity
{
    public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, Guid>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IImageProcessingService _imageProcessingService;

        public CreateActivityCommandHandler(
            IActivityRepository activityRepository,
            IImageProcessingService imageProcessingService)
        {
            _activityRepository = activityRepository;
            _imageProcessingService = imageProcessingService;
        }

        public async Task<Guid> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            // 1️⃣ Date string'den ActivityDate VO'ya dönüştür
            var activityDate = ActivityDate.FromString(request.Date);

            // 2️⃣ Process image - either URL or Base64
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

            // 3️⃣ VideoUrl map et
            Url? videoUrl = null;
            if (!string.IsNullOrWhiteSpace(request.VideoUrl))
                videoUrl = Url.Create(request.VideoUrl);

            // 4️⃣ GalleryImages map et (opsiyonel)
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

            // 5️⃣ Address VO map et
            var address = new Address(
                request.Address.Street,
                request.Address.HouseNo,
                request.Address.ZipCode,
                request.Address.City,
                request.Address.State,
                request.Address.Country
            );

            // 6️⃣ DetailedContent map et (optional)
            LocalizedContent? detailedContentTr = null;
            if (!string.IsNullOrWhiteSpace(request.DetailedContentTr))
                detailedContentTr = new LocalizedContent(request.DetailedContentTr);

            LocalizedContent? detailedContentDe = null;
            if (!string.IsNullOrWhiteSpace(request.DetailedContentDe))
                detailedContentDe = new LocalizedContent(request.DetailedContentDe);

            // 7️⃣ Activity entity yarat
            var activity = Domain.Commons.AggregateRoot.Activity.Create(
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
                videoUrl
            );

            // Update detailed content if provided
            if (detailedContentTr != null || detailedContentDe != null)
            {
                activity.Update(
                    activity.TitleTr,
                    activity.TitleDe,
                    activity.DescriptionTr,
                    activity.DescriptionDe,
                    activity.Date,
                    activity.Address,
                    activity.Category,
                    imageUrl,
                    imageData,
                    galleryImages,
                    videoUrl,
                    request.IsActive,
                    detailedContentTr,
                    detailedContentDe
                );
            }

            await _activityRepository.AddAsync(activity, cancellationToken);

            return activity.Id;
        }
    }
}
