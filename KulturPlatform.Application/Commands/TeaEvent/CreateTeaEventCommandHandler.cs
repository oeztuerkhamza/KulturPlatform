using KulturPlatform.Application.Constants;
using KulturPlatform.Application.Interfaces;
using KulturPlatform.Application.Interfaces.TeaEvent;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KulturPlatform.Application.Commands.TeaEvent
{
    public sealed class CreateTeaEventCommandHandler
        : IRequestHandler<CreateTeaEventCommand, Guid>
    {
        private readonly ITeaEventWriteRepository _writeRepo;
        private readonly IImageService _imageService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateTeaEventCommandHandler> _logger;

        public CreateTeaEventCommandHandler(
            ITeaEventWriteRepository writeRepo,
            IImageService imageService,
            IUnitOfWork unitOfWork,
            ILogger<CreateTeaEventCommandHandler> logger)
        {
            _writeRepo = writeRepo;
            _imageService = imageService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Guid> Handle(
            CreateTeaEventCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating tea event: {TitleTr}", request.TitleTr);

            Url? uploadedImageUrl = null;

            try
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
                    _logger.LogDebug("Processing Base64 image for tea event: {FileName}", request.ImageFileName);

                    // Upload image to storage and get URL
                    var image = await _imageService.ProcessAndUploadImageAsync(
                        request.ImageBase64,
                        request.ImageFileName,
                        ImageProcessingConstants.TeaEvent.ContainerPath,
                        maxWidth: ImageProcessingConstants.TeaEvent.MaxWidth,
                        maxHeight: ImageProcessingConstants.TeaEvent.MaxHeight,
                        quality: ImageProcessingConstants.TeaEvent.Quality,
                        cancellationToken
                    );
                    imageUrl = image.ImageUrl;
                    uploadedImageUrl = imageUrl;
                }
                else if (!string.IsNullOrWhiteSpace(request.ImageUrl))
                {
                    _logger.LogDebug("Using provided URL for tea event image: {Url}", request.ImageUrl);
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
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Tea event created successfully with ID: {TeaEventId}", teaEvent.Id);

                return teaEvent.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create tea event: {TitleTr}", request.TitleTr);

                // Compensation: Clean up uploaded image if DB save failed
                if (uploadedImageUrl != null && !string.IsNullOrWhiteSpace(request.ImageBase64))
                {
                    try
                    {
                        await _imageService.DeleteImageByUrlAsync(uploadedImageUrl.Value, cancellationToken);
                        _logger.LogWarning("Cleaned up orphaned image: {ImageUrl}", uploadedImageUrl.Value);
                    }
                    catch (Exception cleanupEx)
                    {
                        _logger.LogError(cleanupEx, "Failed to cleanup orphaned image: {ImageUrl}", uploadedImageUrl.Value);
                    }
                }

                throw;
            }
        }
    }
}
