using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class CreateCoreValueCommandHandler : IRequestHandler<CreateCoreValueCommand, Guid>
{
    private readonly ICoreValueRepository _repository;
    private readonly IUnitOfWork _uow;

    public CreateCoreValueCommandHandler(ICoreValueRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateCoreValueCommand request, CancellationToken cancellationToken)
    {
        var coreValue = CoreValue.Create(
            new Title(request.TitleTr),
            new Title(request.TitleDe),
            new Description(request.DescriptionTr),
            new Description(request.DescriptionDe),
            request.Order
        );

        await _repository.AddAsync(coreValue, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return coreValue.Id;
    }
}
