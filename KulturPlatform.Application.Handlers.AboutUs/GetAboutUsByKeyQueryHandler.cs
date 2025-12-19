using AutoMapper;
using KulturPlatform.Application.Dtos.AboutUs;
using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Application.Queries.AboutUs;
using MediatR;

namespace KulturPlatform.Application.Handlers.AboutUs
{
    public class GetAboutUsByKeyQueryHandler : IRequestHandler<GetAboutUsByKeyQuery, AboutUsDto?>
    {
        private readonly IAboutUsRepository _repository;
        private readonly IMapper _mapper;

        public GetAboutUsByKeyQueryHandler(IAboutUsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AboutUsDto?> Handle(GetAboutUsByKeyQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByKeyAsync(request.Key, cancellationToken);
            if (entity == null) return null;
            return _mapper.Map<AboutUsDto>(entity);
        }
    }
}
