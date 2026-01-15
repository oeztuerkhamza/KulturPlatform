using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class CreateFocusAreaCommandHandler : IRequestHandler<CreateFocusAreaCommand, Guid>
{
    private readonly IFocusAreaRepository _repository;
    private readonly IUnitOfWork _uow;

    public CreateFocusAreaCommandHandler(IFocusAreaRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateFocusAreaCommand request, CancellationToken cancellationToken)
    {
        var focusArea = FocusArea.Create(
            new Title(request.TitleTr),
            new Title(request.TitleDe),
            new Description(request.DescriptionTr),
            new Description(request.DescriptionDe),
            request.Order
        );

        await _repository.AddAsync(focusArea, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return focusArea.Id;
    }
}
