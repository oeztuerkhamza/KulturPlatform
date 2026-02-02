using KulturPlatform.Application.Interfaces;
using KulturPlatform.Application.Interfaces.Activity;
using KulturPlatform.Application.Services;
using KulturPlatform.Domain.Commons.ValueObjects;
using MediatR;

namespace KulturPlatform.Application.Commands.Activity
{
    public class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, Unit>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ImageService _imageService;

        public UpdateActivityCommandHandler(
            IActivityRepository activityRepository,
            ImageService imageService)
        {
            _activityRepository = activityRepository;
            _imageService = imageService;
        }

        public async Task<Unit> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
        {
            // 1️⃣ Mevcut Activity'yi repository'den çek
            var activity = await _activityRepository.GetByIdAsync(request.Id, cancellationToken);
            if (activity == null)
                throw new KeyNotFoundException($"Activity with Id {request.Id} not found.");

            // 2️⃣ VO'ları oluştur
            var activityDate = ActivityDate.FromString(request.Date);

            // 3️⃣ Process image - upload to storage
            Url? imageUrl = null;

            // Validate: both cannot be provided
            if (!string.IsNullOrWhiteSpace(request.ImageUrl) && !string.IsNullOrWhiteSpace(request.ImageBase64))
                throw new ArgumentException("Cannot provide both ImageUrl and ImageBase64. Choose one.");

            if (!string.IsNullOrWhiteSpace(request.ImageBase64) && !string.IsNullOrWhiteSpace(request.ImageFileName))
            {
                // Get current image for cleanup
                HybridImage? currentImage = null;
                if (activity.ImageUrl != null)
                {
                    currentImage = HybridImage.FromUrl(activity.ImageUrl.Value);
                }
                else if (activity.ImageData != null)
                {
                    currentImage = HybridImage.FromImageData(activity.ImageData);
                }

                // Upload new image to storage
                var newImage = await _imageService.UpdateImageAsync(
                    currentImage,
                    request.ImageBase64,
                    request.ImageFileName,
                    "activities",
                    maxWidth: 1920,
                    maxHeight: 1080,
                    quality: 85
                );
                imageUrl = newImage.ImageUrl;
            }
            else if (!string.IsNullOrWhiteSpace(request.ImageUrl))
            {
                imageUrl = Url.Create(request.ImageUrl);
            }

            // 4️⃣ VideoUrl map et
            Url? videoUrl = null;
            if (!string.IsNullOrWhiteSpace(request.VideoUrl))
                videoUrl = Url.Create(request.VideoUrl);

            // 5️⃣ GalleryImages map et - upload to storage
            MediaGallery? galleryImages = null;
            if (request.GalleryImages != null && request.GalleryImages.Any())
            {
                var galleryImageList = new List<Domain.Commons.ValueObjects.GalleryImage>();
                
                foreach (var dto in request.GalleryImages)
                {
                    if (!string.IsNullOrWhiteSpace(dto.Base64Data) && !string.IsNullOrWhiteSpace(dto.FileName))
                    {
                        // Upload gallery image to storage
                        var galleryImage = await _imageService.ProcessAndUploadImageAsync(
                            dto.Base64Data,
                            dto.FileName,
                            "activities-gallery",
                            maxWidth: 1920,
                            maxHeight: 1080,
                            quality: 80
                        );
                        if (galleryImage.ImageUrl != null)
                        {
                            galleryImageList.Add(Domain.Commons.ValueObjects.GalleryImage.FromUrl(galleryImage.ImageUrl.Value));
                        }
                    }
                    else if (!string.IsNullOrWhiteSpace(dto.Url))
                    {
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
                null, // Always null - using URL storage now
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
