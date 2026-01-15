using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs;

public class UpdateFocusAreaCommandHandler : IRequestHandler<UpdateFocusAreaCommand>
{
    private readonly IFocusAreaRepository _repository;
    private readonly IUnitOfWork _uow;

    public UpdateFocusAreaCommandHandler(IFocusAreaRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task Handle(UpdateFocusAreaCommand request, CancellationToken cancellationToken)
    {
        var focusArea = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (focusArea == null)
            throw new KeyNotFoundException($"FocusArea with Id {request.Id} not found.");

        focusArea.Update(
            new Title(request.TitleTr),
            new Title(request.TitleDe),
            new Description(request.DescriptionTr),
            new Description(request.DescriptionDe),
            request.Order
        );

        await _repository.UpdateAsync(focusArea, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
    }
}
