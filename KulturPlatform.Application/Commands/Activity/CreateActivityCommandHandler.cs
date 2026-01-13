using KulturPlatform.Application.Interfaces.Activity;
using KulturPlatform.Domain.Commons.ValueObjects;
using MediatR;

namespace KulturPlatform.Application.Commands.Activity
{
    public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, Guid>
    {
        private readonly IActivityRepository _activityRepository;

        public CreateActivityCommandHandler(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task<Guid> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            // 1️⃣ Date string’den ActivityDate VO’ya dönüştür
            var activityDate = ActivityDate.FromString(request.Date);

            // 2️⃣ ImageUrl ve VideoUrl string → Url VO map et
            Url? imageUrl = null;
            if (!string.IsNullOrWhiteSpace(request.ImageUrl))
                imageUrl = Url.Create(request.ImageUrl);

            Url? videoUrl = null;
            if (!string.IsNullOrWhiteSpace(request.VideoUrl))
                videoUrl = Url.Create(request.VideoUrl);

            // 3️⃣ GalleryImages map et (opsiyonel)
            MediaGallery? galleryImages = null;
            if (request.GalleryImages != null && request.GalleryImages.Any())
                galleryImages = new MediaGallery(request.GalleryImages);

            // 4️⃣ Address VO map et
            var address = new Address(
                request.Address.Street,
                request.Address.HouseNo,
                request.Address.ZipCode,
                request.Address.City,
                request.Address.State,
                request.Address.Country
            );

            // 6️⃣ Activity entity yarat
            var activity = Domain.Commons.AggregateRoot.Activity.Create(
                new Title(request.TitleTr),
                new Title(request.TitleDe),
                new Description(request.DescriptionTr),
                new Description(request.DescriptionDe),
                activityDate,
                address,
                new Category(request.Category),
                imageUrl,
                galleryImages,
                videoUrl
            );

            await _activityRepository.AddAsync(activity, cancellationToken);

            return activity.Id;
        }
    }
}
