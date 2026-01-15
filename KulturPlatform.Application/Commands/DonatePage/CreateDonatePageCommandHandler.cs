using KulturPlatform.Application.Interfaces.DonatePage;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.DonatePage
{
    public class CreateDonatePageCommandHandler : IRequestHandler<CreateDonatePageCommand, Guid>
    {
        private readonly IDonatePageRepository _repo;
        private readonly IUnitOfWork _uow;

        public CreateDonatePageCommandHandler(IDonatePageRepository repo, IUnitOfWork uow)
        {
            _repo = repo;
            _uow = uow;
        }

        public async Task<Guid> Handle(CreateDonatePageCommand request, CancellationToken cancellationToken)
        {
            var entity = Domain.Commons.Aggregates.DonatePage.CreateNew(
                Title.Create(request.HeroTitleTr),
                Title.Create(request.HeroTitleDe),
                Title.Create(request.HeroSubtitleTr),
                Title.Create(request.HeroSubtitleDe),
                Url.Create(request.HeroImageUrl),
                Title.Create(request.Feature1TitleTr),
                Title.Create(request.Feature1TitleDe),
                Title.Create(request.Feature2TitleTr),
                Title.Create(request.Feature2TitleDe),
                Title.Create(request.Feature3TitleTr),
                Title.Create(request.Feature3TitleDe),
                Title.Create(request.WhyDonateTitleTr),
                Title.Create(request.WhyDonateTitleDe),
                new Description(request.WhyDonateDescriptionTr),
                new Description(request.WhyDonateDescriptionDe),
                Title.Create(request.WhereTitleTr),
                Title.Create(request.WhereTitleDe),
                new Description(request.WhereDescriptionTr),
                new Description(request.WhereDescriptionDe),
                new Description(request.TaxInfoTr),
                new Description(request.TaxInfoDe),
                request.AccountHolder,
                request.Iban,
                request.BicSwift,
                request.BankName,
                Url.Create(request.PayPalUrl),
                request.PayPalHandle,
                request.ContentTr,
                request.ContentDe
            );

            await _repo.AddAsync(entity, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
