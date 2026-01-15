using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class UpdateAboutUsGoalsCommandHandler : IRequestHandler<UpdateAboutUsGoalsCommand>
{
    private readonly IAboutUsGoalsRepository _repository;
    private readonly IUnitOfWork _uow;

    public UpdateAboutUsGoalsCommandHandler(IAboutUsGoalsRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task Handle(UpdateAboutUsGoalsCommand request, CancellationToken cancellationToken)
    {
        var goals = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (goals == null)
            throw new KeyNotFoundException($"AboutUsGoals with Id {request.Id} not found.");

        goals.Update(
            new Description(request.GoalsTr),
            new Description(request.GoalsDe)
        );

        await _repository.UpdateAsync(goals, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
    }
}
