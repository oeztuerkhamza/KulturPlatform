using AutoMapper;
using KulturPlatform.Application.Dtos.AdminDto;
using KulturPlatform.Application.Interfaces.Admin;
using MediatR;

namespace KulturPlatform.Application.Queries.Admin
{
    public class GetAllAdminsQueryHandler : IRequestHandler<GetAllAdminsQuery, IEnumerable<AdminDto>>
    {
        private readonly IAdminReadService _adminReadService;
        private readonly IMapper _mapper;

        public GetAllAdminsQueryHandler(IAdminReadService adminReadService, IMapper mapper)
        {
            _adminReadService = adminReadService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AdminDto>> Handle(GetAllAdminsQuery request, CancellationToken cancellationToken)
        {
            var admins = await _adminReadService.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<AdminDto>>(admins);
        }
    }
}
