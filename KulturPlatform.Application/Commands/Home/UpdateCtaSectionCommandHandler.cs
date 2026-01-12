using KulturPlatform.Application.Interfaces.Home;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.Home
{
    public class UpdateCtaSectionCommandHandler : IRequestHandler<UpdateCtaSectionCommand>
    {
        private readonly ICtaSectionRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCtaSectionCommandHandler(ICtaSectionRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateCtaSectionCommand request, CancellationToken cancellationToken)
        {
            var ctaSection = await _repository.GetByIdAsync(request.Id, cancellationToken)
                             ?? throw new KeyNotFoundException($"CtaSection with Id {request.Id} not found.");

            ctaSection.Update(
                new Title(request.TitleTr),
                new Title(request.TitleDe),
                new Description(request.DescriptionTr),
                new Description(request.DescriptionDe),
                new Title(request.PrimaryButtonTr),
                new Title(request.PrimaryButtonDe),
                new Title(request.SecondaryButtonTr),
                new Title(request.SecondaryButtonDe),
                new Title(request.DonateButtonTr),
                new Title(request.DonateButtonDe)
            );

            _repository.Update(ctaSection, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
