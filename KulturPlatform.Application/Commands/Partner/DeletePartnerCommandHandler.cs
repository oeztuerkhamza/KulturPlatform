using KulturPlatform.Application.Interfaces.Partner;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.Partner
{
    public class DeletePartnerCommandHandler : IRequestHandler<DeletePartnerCommand>
    {
        private readonly IPartnerRepository _partnerRepository;
        private readonly IUnitOfWork _uow;

        public DeletePartnerCommandHandler(IPartnerRepository partnerRepository, IUnitOfWork uow)
        {
            _partnerRepository = partnerRepository;
            _uow = uow;
        }

        public async Task Handle(DeletePartnerCommand request, CancellationToken cancellationToken)
        {
            var partner = await _partnerRepository.GetByIdAsync(request.Id, cancellationToken);
            if (partner == null)
                throw new KeyNotFoundException($"Partner with Id {request.Id} not found.");

            _partnerRepository.Delete(partner, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}
