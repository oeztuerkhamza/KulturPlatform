using AutoMapper;
using KulturPlatform.Application.Dtos.AboutUs;
using KulturPlatform.Application.Interfaces.AboutUs;
using MediatR;

namespace KulturPlatform.Application.Queries.AboutUs
{
    public class GetAboutUsQueryHandler
        : IRequestHandler<GetAboutUsQuery, AboutUsDto>
    {
        private readonly IAboutUsReadRepository _repo;
        private readonly IMapper _mapper;

        public GetAboutUsQueryHandler(
            IAboutUsReadRepository repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<AboutUsDto> Handle(
            GetAboutUsQuery request,
            CancellationToken ct)
        {
            var aboutUs = await _repo.GetAsync(ct);
            return _mapper.Map<AboutUsDto>(aboutUs);
        }
    }

}
