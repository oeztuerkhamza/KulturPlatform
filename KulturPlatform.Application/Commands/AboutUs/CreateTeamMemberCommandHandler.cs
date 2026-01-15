using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class CreateTeamMemberCommandHandler : IRequestHandler<CreateTeamMemberCommand, Guid>
{
    private readonly ITeamMemberRepository _repository;
    private readonly IUnitOfWork _uow;

    public CreateTeamMemberCommandHandler(ITeamMemberRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateTeamMemberCommand request, CancellationToken cancellationToken)
    {
        var teamMember = TeamMember.Create(
            new Name(request.Name),
            new Title(request.TitleTr),
            new Title(request.TitleDe),
            request.DescriptionTr != null ? new Description(request.DescriptionTr) : null,
            request.DescriptionDe != null ? new Description(request.DescriptionDe) : null,
            request.ImageUrl,
            request.Order
        );

        await _repository.AddAsync(teamMember, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return teamMember.Id;
    }
}
