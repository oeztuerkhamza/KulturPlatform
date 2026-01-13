using KulturPlatform.Application.Interfaces.Home;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.Home
{
    public class UpdateHeroSectionCommandHandler : IRequestHandler<UpdateHeroSectionCommand>
    {
        private readonly IHeroSectionRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateHeroSectionCommandHandler(IHeroSectionRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateHeroSectionCommand request, CancellationToken cancellationToken)
        {
            var heroSection = await _repository.GetByIdAsync(request.Id, cancellationToken)
                              ?? throw new KeyNotFoundException($"HeroSection with Id {request.Id} not found.");

            heroSection.Update(
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

            _repository.Update(heroSection, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
