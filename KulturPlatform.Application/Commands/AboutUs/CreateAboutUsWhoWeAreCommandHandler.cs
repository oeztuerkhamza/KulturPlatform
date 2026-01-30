using KulturPlatform.Application.Interfaces;
using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class CreateAboutUsWhoWeAreCommandHandler : IRequestHandler<CreateAboutUsWhoWeAreCommand, Guid>
{
    private readonly IAboutUsWhoWeAreRepository _repository;
    private readonly IUnitOfWork _uow;
    private readonly IImageProcessingService _imageProcessingService;

    public CreateAboutUsWhoWeAreCommandHandler(
        IAboutUsWhoWeAreRepository repository,
        IUnitOfWork uow,
        IImageProcessingService imageProcessingService)
    {
        _repository = repository;
        _uow = uow;
        _imageProcessingService = imageProcessingService;
    }

    public async Task<Guid> Handle(CreateAboutUsWhoWeAreCommand request, CancellationToken cancellationToken)
    {
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

        var whoWeAre = AboutUsWhoWeAre.Create(
            new Description(request.WhoWeAreTr),
            new Description(request.WhoWeAreDe),
            bannerImageUrl,
            bannerImageData
        );

        await _repository.AddAsync(whoWeAre, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return whoWeAre.Id;
    }
}
