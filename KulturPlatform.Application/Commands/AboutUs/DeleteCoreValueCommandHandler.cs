using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class DeleteCoreValueCommandHandler : IRequestHandler<DeleteCoreValueCommand>
{
    private readonly ICoreValueRepository _repository;
    private readonly IUnitOfWork _uow;

    public DeleteCoreValueCommandHandler(ICoreValueRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task Handle(DeleteCoreValueCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
    }
}
