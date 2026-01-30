using KulturPlatform.Application.Interfaces;
using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class UpdateFocusAreaCommandHandler : IRequestHandler<UpdateFocusAreaCommand>
{
    private readonly IFocusAreaRepository _repository;
    private readonly IUnitOfWork _uow;
    private readonly IImageProcessingService _imageProcessingService;

    public UpdateFocusAreaCommandHandler(
        IFocusAreaRepository repository, 
        IUnitOfWork uow,
        IImageProcessingService imageProcessingService)
    {
        _repository = repository;
        _uow = uow;
        _imageProcessingService = imageProcessingService;
    }

    public async Task Handle(UpdateFocusAreaCommand request, CancellationToken cancellationToken)
    {
        var focusArea = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (focusArea == null)
            throw new KeyNotFoundException($"FocusArea with Id {request.Id} not found.");

        // Process hybrid icon
        Url? iconUrl = null;
        ImageData? iconData = null;

        if (!string.IsNullOrWhiteSpace(request.IconBase64) && !string.IsNullOrWhiteSpace(request.IconFileName))
        {
            // Process base64 icon
            iconData = await _imageProcessingService.ProcessImageAsync(
                request.IconBase64,
                request.IconFileName,
                maxWidth: 512,
                maxHeight: 512,
                quality: 85
            );
        }
        else if (!string.IsNullOrWhiteSpace(request.IconUrl))
        {
            // Use URL
            iconUrl = Url.Create(request.IconUrl);
        }

        focusArea.Update(
            new Title(request.TitleTr),
            new Title(request.TitleDe),
            new Description(request.DescriptionTr),
            new Description(request.DescriptionDe),
            iconUrl,
            iconData,
            request.Order
        );

        await _repository.UpdateAsync(focusArea, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
    }
}
