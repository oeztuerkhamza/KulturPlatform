using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class DeleteTeamMemberCommandHandler : IRequestHandler<DeleteTeamMemberCommand>
{
    private readonly ITeamMemberRepository _repository;
    private readonly IUnitOfWork _uow;

    public DeleteTeamMemberCommandHandler(ITeamMemberRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task Handle(DeleteTeamMemberCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
    }
}
