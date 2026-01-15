using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class CreateAboutUsGoalsCommandHandler : IRequestHandler<CreateAboutUsGoalsCommand, Guid>
{
    private readonly IAboutUsGoalsRepository _repository;
    private readonly IUnitOfWork _uow;

    public CreateAboutUsGoalsCommandHandler(IAboutUsGoalsRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateAboutUsGoalsCommand request, CancellationToken cancellationToken)
    {
        var goals = AboutUsGoals.Create(
            new Description(request.GoalsTr),
            new Description(request.GoalsDe)
        );

        await _repository.AddAsync(goals, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return goals.Id;
    }
}
