using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class DeleteActivityAreaCommandHandler : IRequestHandler<DeleteActivityAreaCommand>
{
    private readonly IActivityAreaRepository _repository;
    private readonly IUnitOfWork _uow;

    public DeleteActivityAreaCommandHandler(IActivityAreaRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task Handle(DeleteActivityAreaCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
    }
}
