using KulturPlatform.Application.Interfaces.Partner;
using KulturPlatform.Domain.Commons.ValueObjects;
using MediatR;

namespace KulturPlatform.Application.Commands.Partner
{
    public class CreatePartnerCommandHandler : IRequestHandler<CreatePartnerCommand, Guid>
    {
        private readonly IPartnerRepository _partnerRepository;

        public CreatePartnerCommandHandler(IPartnerRepository partnerRepository)
        {
            _partnerRepository = partnerRepository;
        }

        public async Task<Guid> Handle(CreatePartnerCommand request, CancellationToken cancellationToken)
        {
            var name = new PartnerName(request.Name);
            var displayOrder = new DisplayOrder(request.DisplayOrder);
            var logoUrl = request.LogoUrl != null ? Image.Create(request.LogoUrl) : null;
            var websiteUrl = request.WebsiteUrl != null ? Image.Create(request.WebsiteUrl) : null;

            var partner = Domain.Commons.Aggregates.Partner.CreateNew(name, displayOrder, logoUrl, websiteUrl);

            await _partnerRepository.AddAsync(partner, cancellationToken);

            return partner.Id;
        }
    }
}
