using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class UpdateAboutUsWhoWeAreCommandHandler : IRequestHandler<UpdateAboutUsWhoWeAreCommand>
{
    private readonly IAboutUsWhoWeAreRepository _repository;
    private readonly IUnitOfWork _uow;

    public UpdateAboutUsWhoWeAreCommandHandler(IAboutUsWhoWeAreRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task Handle(UpdateAboutUsWhoWeAreCommand request, CancellationToken cancellationToken)
    {
        var whoWeAre = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (whoWeAre == null)
            throw new KeyNotFoundException($"AboutUsWhoWeAre with Id {request.Id} not found.");

        whoWeAre.Update(
            new Description(request.WhoWeAreTr),
            new Description(request.WhoWeAreDe)
        );

        await _repository.UpdateAsync(whoWeAre, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
    }
}
