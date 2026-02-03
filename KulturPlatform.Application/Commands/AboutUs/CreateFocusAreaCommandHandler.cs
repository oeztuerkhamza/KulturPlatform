using KulturPlatform.Application.Constants;
using KulturPlatform.Application.Interfaces;
using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KulturPlatform.Application.Commands.AboutUs;

public class CreateFocusAreaCommandHandler : IRequestHandler<CreateFocusAreaCommand, Guid>
{
    private readonly IFocusAreaRepository _repository;
    private readonly IUnitOfWork _uow;
    private readonly IImageService _imageService;
    private readonly ILogger<CreateFocusAreaCommandHandler> _logger;

    public CreateFocusAreaCommandHandler(
        IFocusAreaRepository repository,
        IUnitOfWork uow,
        IImageService imageService,
        ILogger<CreateFocusAreaCommandHandler> logger)
    {
        _repository = repository;
        _uow = uow;
        _imageService = imageService;
        _logger = logger;
    }

    public async Task<Guid> Handle(CreateFocusAreaCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating focus area: {TitleTr}", request.TitleTr);

        Url? uploadedIconUrl = null;

        try
        {
            // Handle icon upload
            Url? iconUrl = null;

            if (!string.IsNullOrWhiteSpace(request.IconBase64) && !string.IsNullOrWhiteSpace(request.IconFileName))
            {
                _logger.LogDebug("Processing Base64 icon for focus area: {FileName}", request.IconFileName);

                // Upload icon to storage and get URL
                var icon = await _imageService.ProcessAndUploadImageAsync(
                    request.IconBase64,
                    request.IconFileName,
                    ImageProcessingConstants.FocusArea.ContainerPath,
                    maxWidth: ImageProcessingConstants.FocusArea.MaxWidth,
                    maxHeight: ImageProcessingConstants.FocusArea.MaxHeight,
                    quality: ImageProcessingConstants.FocusArea.Quality,
                    cancellationToken);

                iconUrl = icon.ImageUrl;
                uploadedIconUrl = iconUrl;
            }
            else if (!string.IsNullOrWhiteSpace(request.IconUrl))
            {
                _logger.LogDebug("Using provided URL for focus area icon: {Url}", request.IconUrl);
                iconUrl = Url.Create(request.IconUrl);
            }

            var focusArea = FocusArea.Create(
                new Title(request.TitleTr),
                new Title(request.TitleDe),
                new Description(request.DescriptionTr),
                new Description(request.DescriptionDe),
                iconUrl,
                null, // Always null - using URL storage now
                request.Order
            );

            await _repository.AddAsync(focusArea, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Focus area created successfully with ID: {FocusAreaId}", focusArea.Id);

            return focusArea.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create focus area: {TitleTr}", request.TitleTr);

            // Compensation: Clean up uploaded icon if DB save failed
            if (uploadedIconUrl != null && !string.IsNullOrWhiteSpace(request.IconBase64))
            {
                try
                {
                    await _imageService.DeleteImageByUrlAsync(uploadedIconUrl.Value, cancellationToken);
                    _logger.LogWarning("Cleaned up orphaned icon: {IconUrl}", uploadedIconUrl.Value);
                }
                catch (Exception cleanupEx)
                {
                    _logger.LogError(cleanupEx, "Failed to cleanup orphaned icon: {IconUrl}", uploadedIconUrl.Value);
                }
            }

            throw;
        }
    }
}
