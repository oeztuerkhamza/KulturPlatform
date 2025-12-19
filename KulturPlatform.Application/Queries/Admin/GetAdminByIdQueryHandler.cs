using AutoMapper;
using KulturPlatform.Application.Dtos.AdminDto;
using KulturPlatform.Application.Interfaces.Admin;
using MediatR;

namespace KulturPlatform.Application.Queries.Admin
{
    public class GetAdminByIdQueryHandler : IRequestHandler<GetAdminByIdQuery, AdminDto?>
    {
        private readonly IAdminReadService _adminReadService;
        private readonly IMapper _mapper;

        public GetAdminByIdQueryHandler(IAdminReadService adminReadService, IMapper mapper)
        {
            _adminReadService = adminReadService;
            _mapper = mapper;
        }

        public async Task<AdminDto?> Handle(GetAdminByIdQuery request, CancellationToken cancellationToken)
        {
            var admin = await _adminReadService.GetByIdAsync(request.Id, cancellationToken);
            return admin == null ? null : _mapper.Map<AdminDto>(admin);
        }
    }
}
