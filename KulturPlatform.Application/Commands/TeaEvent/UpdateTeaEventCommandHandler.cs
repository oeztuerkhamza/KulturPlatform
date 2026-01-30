using KulturPlatform.Application.Interfaces;
using KulturPlatform.Application.Interfaces.TeaEvent;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.TeaEvent
{
    public sealed class UpdateTeaEventCommandHandler
        : IRequestHandler<UpdateTeaEventCommand>
    {
        private readonly ITeaEventWriteRepository _writeRepo;
        private readonly IImageProcessingService _imageProcessingService;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTeaEventCommandHandler(
            ITeaEventWriteRepository writeRepo,
            IImageProcessingService imageProcessingService,
            IUnitOfWork unitOfWork)
        {
            _writeRepo = writeRepo;
            _imageProcessingService = imageProcessingService;
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

            // Process hybrid image if provided
            if (!string.IsNullOrWhiteSpace(request.ImageBase64) && !string.IsNullOrWhiteSpace(request.ImageFileName))
            {
                // Process base64 image
                var imageData = await _imageProcessingService.ProcessImageAsync(
                    request.ImageBase64,
                    request.ImageFileName,
                    maxWidth: 1920,
                    maxHeight: 1080,
                    quality: 85
                );
                teaEvent.UpdateImage(null, imageData);
            }
            else if (!string.IsNullOrWhiteSpace(request.ImageUrl))
            {
                // Use URL
                var imageUrl = Url.Create(request.ImageUrl);
                teaEvent.UpdateImage(imageUrl, null);
            }

            // ✅ UnitOfWork ile kaydet
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
