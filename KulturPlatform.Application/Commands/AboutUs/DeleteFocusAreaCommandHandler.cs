using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class DeleteFocusAreaCommandHandler : IRequestHandler<DeleteFocusAreaCommand>
{
    private readonly IFocusAreaRepository _repository;
    private readonly IUnitOfWork _uow;

    public DeleteFocusAreaCommandHandler(IFocusAreaRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task Handle(DeleteFocusAreaCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
    }
}
