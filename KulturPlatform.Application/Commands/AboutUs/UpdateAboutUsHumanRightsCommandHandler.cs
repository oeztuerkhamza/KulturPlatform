using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class UpdateAboutUsHumanRightsCommandHandler : IRequestHandler<UpdateAboutUsHumanRightsCommand>
{
    private readonly IAboutUsHumanRightsRepository _repository;
    private readonly IUnitOfWork _uow;

    public UpdateAboutUsHumanRightsCommandHandler(IAboutUsHumanRightsRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task Handle(UpdateAboutUsHumanRightsCommand request, CancellationToken cancellationToken)
    {
        var humanRights = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (humanRights == null)
            throw new KeyNotFoundException($"AboutUsHumanRights with Id {request.Id} not found.");

        humanRights.Update(
            new Title(request.TitleTr),
            new Title(request.TitleDe),
            new Description(request.DescriptionTr),
            new Description(request.DescriptionDe),
            request.TenkilMuseumUrl,
            request.InstagramUrl
        );

        await _repository.UpdateAsync(humanRights, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
    }
}
