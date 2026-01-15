using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class UpdateActivityAreaCommandHandler : IRequestHandler<UpdateActivityAreaCommand>
{
    private readonly IActivityAreaRepository _repository;
    private readonly IUnitOfWork _uow;

    public UpdateActivityAreaCommandHandler(IActivityAreaRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task Handle(UpdateActivityAreaCommand request, CancellationToken cancellationToken)
    {
        var activityArea = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (activityArea == null)
            throw new KeyNotFoundException($"ActivityArea with Id {request.Id} not found.");

        activityArea.Update(
            new Title(request.TitleTr),
            new Title(request.TitleDe),
            new Description(request.DescriptionTr),
            new Description(request.DescriptionDe),
            request.Order
        );

        await _repository.UpdateAsync(activityArea, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
    }
}
