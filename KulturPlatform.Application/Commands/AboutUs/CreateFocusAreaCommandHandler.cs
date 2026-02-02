using KulturPlatform.Application.Interfaces;
using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Application.Services;
using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class CreateFocusAreaCommandHandler : IRequestHandler<CreateFocusAreaCommand, Guid>
{
    private readonly IFocusAreaRepository _repository;
    private readonly IUnitOfWork _uow;
    private readonly ImageService _imageService;

    public CreateFocusAreaCommandHandler(
        IFocusAreaRepository repository,
        IUnitOfWork uow,
        ImageService imageService)
    {
        _repository = repository;
        _uow = uow;
        _imageService = imageService;
    }

    public async Task<Guid> Handle(CreateFocusAreaCommand request, CancellationToken cancellationToken)
    {
        // Handle icon upload
        Url? iconUrl = null;

        if (!string.IsNullOrWhiteSpace(request.IconBase64) && !string.IsNullOrWhiteSpace(request.IconFileName))
        {
            // Upload icon to storage and get URL
            var icon = await _imageService.ProcessAndUploadImageAsync(
                request.IconBase64,
                request.IconFileName,
                "focus-areas",
                maxWidth: 512,
                maxHeight: 512,
                quality: 90);

            iconUrl = icon.ImageUrl;
        }
        else if (!string.IsNullOrWhiteSpace(request.IconUrl))
        {
            // Use provided URL
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

        return focusArea.Id;
    }
}
