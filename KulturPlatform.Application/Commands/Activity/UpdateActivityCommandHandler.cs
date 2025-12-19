using KulturPlatform.Application.Interfaces.Activity;
using KulturPlatform.Domain.Commons.ValueObjects;
using MediatR;

namespace KulturPlatform.Application.Commands.Activity
{
    public class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand>
    {
        private readonly IActivityRepository _activityRepository;

        public UpdateActivityCommandHandler(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = await _activityRepository.GetByIdAsync(request.Id, cancellationToken);
            if (activity == null)
            {
                throw new KeyNotFoundException($"Activity with Id {request.Id} not found.");
            }

            var titleTr = Title.Create(request.TitleTr);
            var titleDe = Title.Create(request.TitleDe);
            var descriptionTr = new Description(request.DescriptionTr);
            var descriptionDe = new Description(request.DescriptionDe);
            var dateTr = new ActivityDate(request.DateTr, request.DateDe, request.DateISO);
            var dateDe = new ActivityDate(request.DateTr, request.DateDe, request.DateISO);
            var location = new Address(request.Street, request.HouseNo, request.City, request.State, request.Country, request.ZipCode);
            var category = new Category(request.Category);
            var imageUrl = request.ImageUrl != null ? Url.Create(request.ImageUrl) : null;
            var galleryImages = request.GalleryImages != null && request.GalleryImages.Any()
                ? new MediaGallery(request.GalleryImages)
                : null;
            var videoUrl = request.VideoUrl != null ? Url.Create(request.VideoUrl) : null;

            activity.Update(
                titleTr,
                titleDe,
                descriptionTr,
                descriptionDe,
                dateTr,
                dateDe,
                location,
                category,
                imageUrl,
                galleryImages,
                videoUrl,
                request.IsActive,
                request.DetailedContentTr,
                request.DetailedContentDe
            );

            _activityRepository.Update(activity, cancellationToken);
        }
    }
}
