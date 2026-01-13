using KulturPlatform.Application.Interfaces.Home;
using KulturPlatform.Domain.Commons.AggregateRoot;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.Home
{
    public class CreateHeroSectionCommandHandler : IRequestHandler<CreateHeroSectionCommand, Guid>
    {
        private readonly IHeroSectionRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateHeroSectionCommandHandler(IHeroSectionRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateHeroSectionCommand request, CancellationToken cancellationToken)
        {
            var heroSection = HeroSection.Create(
                new Title(request.TitleTr),
                new Title(request.TitleDe),
                new Title(request.SubtitleTr),
                new Title(request.SubtitleDe),
                new Description(request.DescriptionTr),
                new Description(request.DescriptionDe),
                Image.Create(request.BackgroundImageUrl),
                new Title(request.PrimaryButtonTextTr),
                new Title(request.PrimaryButtonTextDe),
                new Title(request.SecondaryButtonTextTr),
                new Title(request.SecondaryButtonTextDe)
            );

            await _repository.AddAsync(heroSection, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return heroSection.Id;
        }
    }
}
