using KulturPlatform.Application.Interfaces;
using KulturPlatform.Application.Interfaces.TeaEvent;
using KulturPlatform.Domain.Commons.ValueObjects;
using MediatR;

namespace KulturPlatform.Application.Commands.TeaEvent
{
    public sealed class CreateTeaEventCommandHandler
        : IRequestHandler<CreateTeaEventCommand, Guid>
    {
        private readonly ITeaEventWriteRepository _writeRepo;
        private readonly IImageProcessingService _imageProcessingService;

        public CreateTeaEventCommandHandler(
            ITeaEventWriteRepository writeRepo,
            IImageProcessingService imageProcessingService)
        {
            _writeRepo = writeRepo;
            _imageProcessingService = imageProcessingService;
        }

        public async Task<Guid> Handle(
            CreateTeaEventCommand request,
            CancellationToken cancellationToken)
        {
            var content = TeaEventContent.Create(
                request.IntroTr,
                request.IntroDe,
                request.HeritageTextTr,
                request.HeritageTextDe,
                request.ParticipationTextTr,
                request.ParticipationTextDe,
                request.ContactEmail
            );

            // Process hybrid image
            Url? imageUrl = null;
            ImageData? imageData = null;

            if (!string.IsNullOrWhiteSpace(request.ImageBase64) && !string.IsNullOrWhiteSpace(request.ImageFileName))
            {
                // Process base64 image
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
                // Use URL
                imageUrl = Url.Create(request.ImageUrl);
            }

            var teaEvent = Domain.Commons.Aggregates.TeaEvent.CreateNew(
                Title.Create(request.TitleTr),
                Title.Create(request.TitleDe),
                content,
                request.Date,
                request.Time,
                Location.Create(request.Location),
                imageUrl,
                imageData
            );

            await _writeRepo.AddAsync(teaEvent, cancellationToken);
            return teaEvent.Id;
        }
    }
}
