using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class CreateActivityAreaCommandHandler : IRequestHandler<CreateActivityAreaCommand, Guid>
{
    private readonly IActivityAreaRepository _repository;
    private readonly IUnitOfWork _uow;

    public CreateActivityAreaCommandHandler(IActivityAreaRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateActivityAreaCommand request, CancellationToken cancellationToken)
    {
        var activityArea = ActivityArea.Create(
            new Title(request.TitleTr),
            new Title(request.TitleDe),
            new Description(request.DescriptionTr),
            new Description(request.DescriptionDe),
            request.Order
        );

        await _repository.AddAsync(activityArea, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return activityArea.Id;
    }
}
