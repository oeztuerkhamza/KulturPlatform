using KulturPlatform.Application.Interfaces.Partner;
using MediatR;

namespace KulturPlatform.Application.Commands.Partner
{
    public class DeletePartnerCommandHandler : IRequestHandler<DeletePartnerCommand>
    {
        private readonly IPartnerRepository _partnerRepository;

        public DeletePartnerCommandHandler(IPartnerRepository partnerRepository)
        {
            _partnerRepository = partnerRepository;
        }

        public async Task Handle(DeletePartnerCommand request, CancellationToken cancellationToken)
        {
            var partner = await _partnerRepository.GetByIdAsync(request.Id, cancellationToken);
            if (partner == null)
                throw new KeyNotFoundException($"Partner with Id {request.Id} not found.");

            _partnerRepository.Delete(partner, cancellationToken);
        }
    }
}
