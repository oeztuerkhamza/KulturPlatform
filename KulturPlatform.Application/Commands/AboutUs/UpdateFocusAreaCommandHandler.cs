using KulturPlatform.Application.Interfaces;
using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Application.Services;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class UpdateFocusAreaCommandHandler : IRequestHandler<UpdateFocusAreaCommand>
{
    private readonly IFocusAreaRepository _repository;
    private readonly IUnitOfWork _uow;
    private readonly ImageService _imageService;

    public UpdateFocusAreaCommandHandler(
        IFocusAreaRepository repository, 
        IUnitOfWork uow,
        ImageService imageService)
    {
        _repository = repository;
        _uow = uow;
        _imageService = imageService;
    }

    public async Task Handle(UpdateFocusAreaCommand request, CancellationToken cancellationToken)
    {
        var focusArea = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (focusArea == null)
            throw new KeyNotFoundException($"FocusArea with Id {request.Id} not found.");

        // Handle icon upload
        Url? iconUrl = null;

        if (!string.IsNullOrWhiteSpace(request.IconBase64) && !string.IsNullOrWhiteSpace(request.IconFileName))
        {
            // Get current icon for cleanup
            HybridImage? currentIcon = null;
            if (focusArea.IconUrl != null)
            {
                currentIcon = HybridImage.FromUrl(focusArea.IconUrl.Value);
            }
            else if (focusArea.IconData != null)
            {
                currentIcon = HybridImage.FromImageData(focusArea.IconData);
            }

            // Upload new icon to storage and get URL
            var newIcon = await _imageService.UpdateImageAsync(
                currentIcon,
                request.IconBase64,
                request.IconFileName,
                "focus-areas",
                maxWidth: 512,
                maxHeight: 512,
                quality: 90);

            iconUrl = newIcon.ImageUrl;
        }
        else if (!string.IsNullOrWhiteSpace(request.IconUrl))
        {
            // Use provided URL
            iconUrl = Url.Create(request.IconUrl);
        }

        focusArea.Update(
            new Title(request.TitleTr),
            new Title(request.TitleDe),
            new Description(request.DescriptionTr),
            new Description(request.DescriptionDe),
            iconUrl,
            null, // Always null - using URL storage now
            request.Order
        );

        await _repository.UpdateAsync(focusArea, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
    }
}
