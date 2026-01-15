using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class CreateAboutUsVisionCommandHandler : IRequestHandler<CreateAboutUsVisionCommand, Guid>
{
    private readonly IAboutUsVisionRepository _repository;
    private readonly IUnitOfWork _uow;

    public CreateAboutUsVisionCommandHandler(IAboutUsVisionRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateAboutUsVisionCommand request, CancellationToken cancellationToken)
    {
        var vision = AboutUsVision.Create(
            new Description(request.VisionTr),
            new Description(request.VisionDe)
        );

        await _repository.AddAsync(vision, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return vision.Id;
    }
}
