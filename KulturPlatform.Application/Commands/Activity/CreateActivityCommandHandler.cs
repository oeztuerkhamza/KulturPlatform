using KulturPlatform.Application.Constants;
using KulturPlatform.Application.Interfaces;
using KulturPlatform.Application.Interfaces.Activity;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KulturPlatform.Application.Commands.Activity
{
    public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, Guid>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IImageService _imageService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateActivityCommandHandler> _logger;

        public CreateActivityCommandHandler(
            IActivityRepository activityRepository,
            IImageService imageService,
            IUnitOfWork unitOfWork,
            ILogger<CreateActivityCommandHandler> logger)
        {
            _activityRepository = activityRepository;
            _imageService = imageService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating activity: {TitleTr}", request.TitleTr);

            var uploadedImageUrls = new List<string>();

            try
            {
                // 1️⃣ Date string'den ActivityDate VO'ya dönüştür
                var activityDate = ActivityDate.FromString(request.Date);

                // 2️⃣ Process image - upload to storage
                Url? imageUrl = null;

                if (!string.IsNullOrWhiteSpace(request.ImageBase64) && !string.IsNullOrWhiteSpace(request.ImageFileName))
                {
                    _logger.LogDebug("Processing Base64 image for activity: {FileName}", request.ImageFileName);

                    // Upload to storage and get URL
                    var image = await _imageService.ProcessAndUploadImageAsync(
                        request.ImageBase64,
                        request.ImageFileName,
                        ImageProcessingConstants.Activity.ContainerPath,
                        maxWidth: ImageProcessingConstants.Activity.MainImageMaxWidth,
                        maxHeight: ImageProcessingConstants.Activity.MainImageMaxHeight,
                        quality: ImageProcessingConstants.Activity.Quality,
                        cancellationToken
                    );
                    imageUrl = image.ImageUrl;
                    if (imageUrl != null) uploadedImageUrls.Add(imageUrl.Value);
                }
                else if (!string.IsNullOrWhiteSpace(request.ImageUrl))
                {
                    imageUrl = Url.Create(request.ImageUrl);
                }

                // 3️⃣ VideoUrl map et
                Url? videoUrl = null;
                if (!string.IsNullOrWhiteSpace(request.VideoUrl))
                    videoUrl = Url.Create(request.VideoUrl);

                // 4️⃣ GalleryImages map et (opsiyonel) - upload to storage
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
                                $"{ImageProcessingConstants.Activity.ContainerPath}-gallery",
                                maxWidth: ImageProcessingConstants.Activity.GalleryMaxWidth,
                                maxHeight: ImageProcessingConstants.Activity.GalleryMaxHeight,
                                quality: ImageProcessingConstants.Activity.Quality,
                                cancellationToken
                            );
                            if (galleryImage.ImageUrl != null)
                            {
                                galleryImageList.Add(Domain.Commons.ValueObjects.GalleryImage.FromUrl(galleryImage.ImageUrl.Value));
                                uploadedImageUrls.Add(galleryImage.ImageUrl.Value);
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
                    null, // Always null - using URL storage now
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
                        null, // Always null - using URL storage now
                        galleryImages,
                        videoUrl,
                        request.IsActive,
                        detailedContentTr,
                        detailedContentDe
                    );
                }

                await _activityRepository.AddAsync(activity, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Activity created successfully with ID: {ActivityId}", activity.Id);

                return activity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create activity: {TitleTr}", request.TitleTr);

                // Compensation: Clean up uploaded images if DB save failed
                foreach (var imageUrl in uploadedImageUrls)
                {
                    try
                    {
                        await _imageService.DeleteImageByUrlAsync(imageUrl, cancellationToken);
                        _logger.LogWarning("Cleaned up orphaned image: {ImageUrl}", imageUrl);
                    }
                    catch (Exception cleanupEx)
                    {
                        _logger.LogError(cleanupEx, "Failed to cleanup orphaned image: {ImageUrl}", imageUrl);
                    }
                }

                throw;
            }
        }
    }
}
