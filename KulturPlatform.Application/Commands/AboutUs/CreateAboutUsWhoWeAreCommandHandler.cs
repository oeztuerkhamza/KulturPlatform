using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class CreateAboutUsWhoWeAreCommandHandler : IRequestHandler<CreateAboutUsWhoWeAreCommand, Guid>
{
    private readonly IAboutUsWhoWeAreRepository _repository;
    private readonly IUnitOfWork _uow;

    public CreateAboutUsWhoWeAreCommandHandler(IAboutUsWhoWeAreRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateAboutUsWhoWeAreCommand request, CancellationToken cancellationToken)
    {
        var whoWeAre = AboutUsWhoWeAre.Create(
            new Description(request.WhoWeAreTr),
            new Description(request.WhoWeAreDe)
        );

        await _repository.AddAsync(whoWeAre, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return whoWeAre.Id;
    }
}
