using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class UpdateCoreValueCommandHandler : IRequestHandler<UpdateCoreValueCommand>
{
    private readonly ICoreValueRepository _repository;
    private readonly IUnitOfWork _uow;

    public UpdateCoreValueCommandHandler(ICoreValueRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task Handle(UpdateCoreValueCommand request, CancellationToken cancellationToken)
    {
        var coreValue = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (coreValue == null)
            throw new KeyNotFoundException($"CoreValue with Id {request.Id} not found.");

        coreValue.Update(
            new Title(request.TitleTr),
            new Title(request.TitleDe),
            new Description(request.DescriptionTr),
            new Description(request.DescriptionDe),
            request.Order
        );

        await _repository.UpdateAsync(coreValue, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
    }
}
