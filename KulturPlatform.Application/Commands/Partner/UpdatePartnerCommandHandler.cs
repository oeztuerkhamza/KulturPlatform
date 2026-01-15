using KulturPlatform.Application.Interfaces.Partner;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.Partner
{
    public class UpdatePartnerCommandHandler : IRequestHandler<UpdatePartnerCommand>
    {
        private readonly IPartnerRepository _partnerRepository;
        private readonly IUnitOfWork _uow;

        public UpdatePartnerCommandHandler(IPartnerRepository partnerRepository, IUnitOfWork uow)
        {
            _partnerRepository = partnerRepository;
            _uow = uow;
        }

        public async Task Handle(UpdatePartnerCommand request, CancellationToken cancellationToken)
        {
            var partner = await _partnerRepository.GetByIdAsync(request.Id, cancellationToken);
            if (partner == null)
            {
                throw new KeyNotFoundException($"Partner with Id {request.Id} not found.");
            }

            var name = new PartnerName(request.Name);
            var descriptionTr = new Description(request.DescriptionTr);
            var descriptionDe = new Description(request.DescriptionDe);
            var displayOrder = new DisplayOrder(request.DisplayOrder);
            var logoUrl = request.LogoUrl != null ? Image.Create(request.LogoUrl) : null;
            var websiteUrl = request.WebsiteUrl != null ? Image.Create(request.WebsiteUrl) : null;

            partner.Update(name, descriptionTr, descriptionDe, displayOrder, logoUrl, websiteUrl);

            if (request.IsActive)
                partner.Activate();
            else
                partner.Deactivate();

            _partnerRepository.Update(partner, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}
