using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class UpdateTeamMemberCommandHandler : IRequestHandler<UpdateTeamMemberCommand>
{
    private readonly ITeamMemberRepository _repository;
    private readonly IUnitOfWork _uow;

    public UpdateTeamMemberCommandHandler(ITeamMemberRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task Handle(UpdateTeamMemberCommand request, CancellationToken cancellationToken)
    {
        var teamMember = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (teamMember == null)
            throw new KeyNotFoundException($"TeamMember with Id {request.Id} not found.");

        teamMember.Update(
            new Name(request.Name),
            new Title(request.TitleTr),
            new Title(request.TitleDe),
            request.DescriptionTr != null ? new Description(request.DescriptionTr) : null,
            request.DescriptionDe != null ? new Description(request.DescriptionDe) : null,
            request.ImageUrl,
            request.Order
        );

        await _repository.UpdateAsync(teamMember, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
    }
}
