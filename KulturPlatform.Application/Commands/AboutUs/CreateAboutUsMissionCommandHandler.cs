using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class CreateAboutUsMissionCommandHandler : IRequestHandler<CreateAboutUsMissionCommand, Guid>
{
    private readonly IAboutUsMissionRepository _repository;
    private readonly IUnitOfWork _uow;

    public CreateAboutUsMissionCommandHandler(IAboutUsMissionRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateAboutUsMissionCommand request, CancellationToken cancellationToken)
    {
        var mission = AboutUsMission.Create(
            new Description(request.MissionTr),
            new Description(request.MissionDe)
        );

        await _repository.AddAsync(mission, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return mission.Id;
    }
}
