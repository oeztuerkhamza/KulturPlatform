using AutoMapper;
using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Interfaces.Partner;
using MediatR;

namespace KulturPlatform.Application.Queries.Partner
{
    public class GetPartnerByIdQueryHandler : IRequestHandler<GetPartnerByIdQuery, PartnerDto?>
    {
        private readonly IPartnerRepository _partnerRepository;
        private readonly IMapper _mapper;

        public GetPartnerByIdQueryHandler(IPartnerRepository partnerRepository, IMapper mapper)
        {
            _partnerRepository = partnerRepository;
            _mapper = mapper;
        }

        public async Task<PartnerDto?> Handle(GetPartnerByIdQuery request, CancellationToken cancellationToken)
        {
            var partner = await _partnerRepository.GetByIdAsync(request.Id, cancellationToken);
            if (partner == null) return null;

            return _mapper.Map<PartnerDto>(partner);
        }
    }
}
