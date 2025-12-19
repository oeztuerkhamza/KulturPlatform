using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Interfaces.Partner;
using MediatR;

namespace KulturPlatform.Application.Queries.Partner
{
    public class GetAllPartnersQueryHandler : IRequestHandler<GetAllPartnersQuery, IEnumerable<PartnerDto>>
    {
        private readonly IPartnerReadService _partnerReadService;

        public GetAllPartnersQueryHandler(IPartnerReadService partnerReadService)
        {
            _partnerReadService = partnerReadService;
        }

        public async Task<IEnumerable<PartnerDto>> Handle(GetAllPartnersQuery request, CancellationToken cancellationToken)
        {
            return await _partnerReadService.GetAllAsync();
        }
    }
}
