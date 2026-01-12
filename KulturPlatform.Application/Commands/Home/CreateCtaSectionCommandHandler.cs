using KulturPlatform.Application.Interfaces.Home;
using KulturPlatform.Domain.Commons.AggregateRoot;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.Home
{
    public class CreateCtaSectionCommandHandler : IRequestHandler<CreateCtaSectionCommand, Guid>
    {
        private readonly ICtaSectionRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCtaSectionCommandHandler(ICtaSectionRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateCtaSectionCommand request, CancellationToken cancellationToken)
        {
            var ctaSection = CtaSection.Create(
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

            await _repository.AddAsync(ctaSection, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ctaSection.Id;
        }
    }
}
