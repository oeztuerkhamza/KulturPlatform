using KulturPlatform.Application.Interfaces.Partner;
using KulturPlatform.Domain.Commons.ValueObjects;
using MediatR;

namespace KulturPlatform.Application.Commands.Partner
{
    public class UpdatePartnerCommandHandler : IRequestHandler<UpdatePartnerCommand>
    {
        private readonly IPartnerRepository _partnerRepository;

        public UpdatePartnerCommandHandler(IPartnerRepository partnerRepository)
        {
            _partnerRepository = partnerRepository;
        }

        public async Task Handle(UpdatePartnerCommand request, CancellationToken cancellationToken)
        {
            var partner = await _partnerRepository.GetByIdAsync(request.Id, cancellationToken);
            if (partner == null)
            {
                throw new KeyNotFoundException($"Partner with Id {request.Id} not found.");
            }

            var name = new PartnerName(request.Name);
            var displayOrder = new DisplayOrder(request.DisplayOrder);
            var logoUrl = request.LogoUrl != null ? Url.Create(request.LogoUrl) : null;
            var websiteUrl = request.WebsiteUrl != null ? Url.Create(request.WebsiteUrl) : null;

            partner.Update(name, displayOrder, logoUrl, websiteUrl);

            if (request.IsActive)
                partner.Activate();
            else
                partner.Deactivate();

            _partnerRepository.Update(partner, cancellationToken);
        }
    }
}
