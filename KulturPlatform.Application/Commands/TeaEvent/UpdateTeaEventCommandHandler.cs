using KulturPlatform.Application.Interfaces;
using KulturPlatform.Application.Interfaces.TeaEvent;
using KulturPlatform.Application.Services;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.TeaEvent
{
    public sealed class UpdateTeaEventCommandHandler
        : IRequestHandler<UpdateTeaEventCommand>
    {
        private readonly ITeaEventWriteRepository _writeRepo;
        private readonly ImageService _imageService;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTeaEventCommandHandler(
            ITeaEventWriteRepository writeRepo,
            ImageService imageService,
            IUnitOfWork unitOfWork)
        {
            _writeRepo = writeRepo;
            _imageService = imageService;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(
            UpdateTeaEventCommand request,
            CancellationToken cancellationToken)
        {
            // ✅ Write repository'den tracking ile al
            var teaEvent = await _writeRepo.GetByIdForUpdateAsync(request.Id, cancellationToken)
                           ?? throw new Exception("TeaEvent not found");

            var content = TeaEventContent.Create(
                request.IntroTr,
                request.IntroDe,
                request.HeritageTextTr,
                request.HeritageTextDe,
                request.ParticipationTextTr,
                request.ParticipationTextDe,
                request.ContactEmail
            );

            teaEvent.UpdateTitle(
                Title.Create(request.TitleTr),
                Title.Create(request.TitleDe)
            );

            teaEvent.UpdateContent(content);
            teaEvent.UpdateLocation(Location.Create(request.Location));
            teaEvent.Reschedule(request.Date, request.Time);

            // Process hybrid image if provided - upload to storage
            if (!string.IsNullOrWhiteSpace(request.ImageBase64) && !string.IsNullOrWhiteSpace(request.ImageFileName))
            {
                // Get current image for cleanup
                HybridImage? currentImage = null;
                if (teaEvent.ImageUrl != null)
                {
                    currentImage = HybridImage.FromUrl(teaEvent.ImageUrl.Value);
                }
                else if (teaEvent.ImageData != null)
                {
                    currentImage = HybridImage.FromImageData(teaEvent.ImageData);
                }

                // Upload new image to storage
                var newImage = await _imageService.UpdateImageAsync(
                    currentImage,
                    request.ImageBase64,
                    request.ImageFileName,
                    "tea-events",
                    maxWidth: 1920,
                    maxHeight: 1080,
                    quality: 85
                );
                teaEvent.UpdateImage(newImage.ImageUrl, null);
            }
            else if (!string.IsNullOrWhiteSpace(request.ImageUrl))
            {
                var imageUrl = Url.Create(request.ImageUrl);
                teaEvent.UpdateImage(imageUrl, null);
            }

            // ✅ UnitOfWork ile kaydet
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
