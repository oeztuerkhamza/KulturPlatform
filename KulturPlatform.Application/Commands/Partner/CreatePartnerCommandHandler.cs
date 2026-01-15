using KulturPlatform.Application.Interfaces.Partner;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.Partner
{
    public class CreatePartnerCommandHandler : IRequestHandler<CreatePartnerCommand, Guid>
    {
        private readonly IPartnerRepository _partnerRepository;
        private readonly IUnitOfWork _uow;

        public CreatePartnerCommandHandler(IPartnerRepository partnerRepository, IUnitOfWork uow)
        {
            _partnerRepository = partnerRepository;
            _uow = uow;
        }

        public async Task<Guid> Handle(CreatePartnerCommand request, CancellationToken cancellationToken)
        {
            var name = new PartnerName(request.Name);
            var descriptionTr = new Description(request.DescriptionTr);
            var descriptionDe = new Description(request.DescriptionDe);
            var displayOrder = new DisplayOrder(request.DisplayOrder);
            var logoUrl = request.LogoUrl != null ? Image.Create(request.LogoUrl) : null;
            var websiteUrl = request.WebsiteUrl != null ? Image.Create(request.WebsiteUrl) : null;

            var partner = Domain.Commons.Aggregates.Partner.CreateNew(name, descriptionTr, descriptionDe, displayOrder, logoUrl, websiteUrl);

            await _partnerRepository.AddAsync(partner, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);

            return partner.Id;
        }
    }
}
