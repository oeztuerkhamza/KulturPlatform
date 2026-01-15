using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class UpdateAboutUsMissionCommandHandler : IRequestHandler<UpdateAboutUsMissionCommand>
{
    private readonly IAboutUsMissionRepository _repository;
    private readonly IUnitOfWork _uow;

    public UpdateAboutUsMissionCommandHandler(IAboutUsMissionRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task Handle(UpdateAboutUsMissionCommand request, CancellationToken cancellationToken)
    {
        var mission = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (mission == null)
            throw new KeyNotFoundException($"AboutUsMission with Id {request.Id} not found.");

        mission.Update(
            new Description(request.MissionTr),
            new Description(request.MissionDe)
        );

        await _repository.UpdateAsync(mission, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
    }
}
