using KulturPlatform.Application.Interfaces;
using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class UpdateAboutUsWhoWeAreCommandHandler : IRequestHandler<UpdateAboutUsWhoWeAreCommand>
{
    private readonly IAboutUsWhoWeAreRepository _repository;
    private readonly IUnitOfWork _uow;
    private readonly IImageProcessingService _imageProcessingService;

    public UpdateAboutUsWhoWeAreCommandHandler(
        IAboutUsWhoWeAreRepository repository, 
        IUnitOfWork uow,
        IImageProcessingService imageProcessingService)
    {
        _repository = repository;
        _uow = uow;
        _imageProcessingService = imageProcessingService;
    }

    public async Task Handle(UpdateAboutUsWhoWeAreCommand request, CancellationToken cancellationToken)
    {
        var whoWeAre = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (whoWeAre == null)
            throw new KeyNotFoundException($"AboutUsWhoWeAre with Id {request.Id} not found.");

        // Process hybrid image
        Url? bannerImageUrl = null;
        ImageData? bannerImageData = null;

        if (!string.IsNullOrWhiteSpace(request.BannerImageBase64) && !string.IsNullOrWhiteSpace(request.BannerImageFileName))
        {
            // Process base64 image
            bannerImageData = await _imageProcessingService.ProcessImageAsync(
                request.BannerImageBase64,
                request.BannerImageFileName,
                maxWidth: 1920,
                maxHeight: 1080,
                quality: 85
            );
        }
        else if (!string.IsNullOrWhiteSpace(request.BannerImageUrl))
        {
            // Use URL
            bannerImageUrl = Url.Create(request.BannerImageUrl);
        }

        whoWeAre.Update(
            new Description(request.WhoWeAreTr),
            new Description(request.WhoWeAreDe),
            bannerImageUrl,
            bannerImageData
        );

        await _repository.UpdateAsync(whoWeAre, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
    }
}
