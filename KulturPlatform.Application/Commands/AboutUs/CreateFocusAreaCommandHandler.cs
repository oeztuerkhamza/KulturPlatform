using KulturPlatform.Application.Interfaces;
using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class CreateFocusAreaCommandHandler : IRequestHandler<CreateFocusAreaCommand, Guid>
{
    private readonly IFocusAreaRepository _repository;
    private readonly IUnitOfWork _uow;
    private readonly IImageProcessingService _imageProcessingService;

    public CreateFocusAreaCommandHandler(
        IFocusAreaRepository repository,
        IUnitOfWork uow,
        IImageProcessingService imageProcessingService)
    {
        _repository = repository;
        _uow = uow;
        _imageProcessingService = imageProcessingService;
    }

    public async Task<Guid> Handle(CreateFocusAreaCommand request, CancellationToken cancellationToken)
    {
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

        var focusArea = FocusArea.Create(
            new Title(request.TitleTr),
            new Title(request.TitleDe),
            new Description(request.DescriptionTr),
            new Description(request.DescriptionDe),
            iconUrl,
            iconData,
            request.Order
        );

        await _repository.AddAsync(focusArea, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return focusArea.Id;
    }
}
