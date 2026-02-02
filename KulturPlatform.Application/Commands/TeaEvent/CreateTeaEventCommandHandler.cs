using KulturPlatform.Application.Interfaces;
using KulturPlatform.Application.Interfaces.TeaEvent;
using KulturPlatform.Application.Services;
using KulturPlatform.Domain.Commons.ValueObjects;
using MediatR;

namespace KulturPlatform.Application.Commands.TeaEvent
{
    public sealed class CreateTeaEventCommandHandler
        : IRequestHandler<CreateTeaEventCommand, Guid>
    {
        private readonly ITeaEventWriteRepository _writeRepo;
        private readonly ImageService _imageService;

        public CreateTeaEventCommandHandler(
            ITeaEventWriteRepository writeRepo,
            ImageService imageService)
        {
            _writeRepo = writeRepo;
            _imageService = imageService;
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

            // Process hybrid image - upload to storage
            Url? imageUrl = null;

            if (!string.IsNullOrWhiteSpace(request.ImageBase64) && !string.IsNullOrWhiteSpace(request.ImageFileName))
            {
                // Upload image to storage and get URL
                var image = await _imageService.ProcessAndUploadImageAsync(
                    request.ImageBase64,
                    request.ImageFileName,
                    "tea-events",
                    maxWidth: 1920,
                    maxHeight: 1080,
                    quality: 85
                );
                imageUrl = image.ImageUrl;
            }
            else if (!string.IsNullOrWhiteSpace(request.ImageUrl))
            {
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
                null // Always null - using URL storage now
            );

            await _writeRepo.AddAsync(teaEvent, cancellationToken);
            return teaEvent.Id;
        }
    }
}
