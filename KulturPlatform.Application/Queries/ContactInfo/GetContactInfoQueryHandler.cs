using AutoMapper;
using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Interfaces.ContactInfo;
using MediatR;

namespace KulturPlatform.Application.Queries.ContactInfo
{
    public class GetContactInfoQueryHandler : IRequestHandler<GetContactInfoQuery, ContactInfoDto?>
    {
        private readonly IContactInfoReadService _readService;
        private readonly IMapper _mapper;

        public GetContactInfoQueryHandler(IContactInfoReadService readService, IMapper mapper)
        {
            _readService = readService;
            _mapper = mapper;
        }

        public async Task<ContactInfoDto?> Handle(GetContactInfoQuery request, CancellationToken cancellationToken)
        {
            // Get the first (and should be only) contact info record
            var contactInfo = await _readService.GetContactInfoAsync(cancellationToken);
            return contactInfo != null ? _mapper.Map<ContactInfoDto>(contactInfo) : null;
        }
    }
}
