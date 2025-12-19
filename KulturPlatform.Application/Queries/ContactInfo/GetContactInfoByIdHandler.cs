using AutoMapper;
using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Interfaces.ContactInfo;
using MediatR;

namespace KulturPlatform.Application.Queries.ContactInfo
{
    public class GetContactInfoByIdHandler : IRequestHandler<GetContactInfoByIdQuery, ContactInfoDto?>
    {
        private readonly IContactInfoReadService _readService;
        private readonly IMapper _mapper;
        public GetContactInfoByIdHandler(IContactInfoReadService readService, IMapper mapper)
        {
            _readService = readService;
            _mapper = mapper;
        }
        public async Task<ContactInfoDto?> Handle(GetContactInfoByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _readService.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null)
                return null;

            return _mapper.Map<ContactInfoDto>(entity);
        }
    }
}
