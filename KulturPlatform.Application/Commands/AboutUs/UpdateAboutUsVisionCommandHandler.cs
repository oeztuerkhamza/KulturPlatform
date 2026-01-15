using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class UpdateAboutUsVisionCommandHandler : IRequestHandler<UpdateAboutUsVisionCommand>
{
    private readonly IAboutUsVisionRepository _repository;
    private readonly IUnitOfWork _uow;

    public UpdateAboutUsVisionCommandHandler(IAboutUsVisionRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task Handle(UpdateAboutUsVisionCommand request, CancellationToken cancellationToken)
    {
        var vision = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (vision == null)
            throw new KeyNotFoundException($"AboutUsVision with Id {request.Id} not found.");

        vision.Update(
            new Description(request.VisionTr),
            new Description(request.VisionDe)
        );

        await _repository.UpdateAsync(vision, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
    }
}
