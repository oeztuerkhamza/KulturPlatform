using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class CreateAboutUsHumanRightsCommandHandler : IRequestHandler<CreateAboutUsHumanRightsCommand, Guid>
{
    private readonly IAboutUsHumanRightsRepository _repository;
    private readonly IUnitOfWork _uow;

    public CreateAboutUsHumanRightsCommandHandler(IAboutUsHumanRightsRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateAboutUsHumanRightsCommand request, CancellationToken cancellationToken)
    {
        var humanRights = AboutUsHumanRights.Create(
            new Title(request.TitleTr),
            new Title(request.TitleDe),
            new Description(request.DescriptionTr),
            new Description(request.DescriptionDe),
            request.TenkilMuseumUrl,
            request.InstagramUrl
        );

        await _repository.AddAsync(humanRights, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return humanRights.Id;
    }
}
