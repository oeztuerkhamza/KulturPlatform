using AutoMapper;
using KulturPlatform.Application.Dtos.AboutUs;
using KulturPlatform.Application.Interfaces.AboutUs;
using MediatR;

namespace KulturPlatform.Application.Queries.AboutUs;

public class GetAboutUsHumanRightsQueryHandler : IRequestHandler<GetAboutUsHumanRightsQuery, AboutUsHumanRightsDto?>
{
    private readonly IAboutUsHumanRightsRepository _repository;
    private readonly IMapper _mapper;

    public GetAboutUsHumanRightsQueryHandler(IAboutUsHumanRightsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<AboutUsHumanRightsDto?> Handle(GetAboutUsHumanRightsQuery request, CancellationToken cancellationToken)
    {
        var humanRights = await _repository.GetAsync(cancellationToken);
        return humanRights != null ? _mapper.Map<AboutUsHumanRightsDto>(humanRights) : null;
    }
}
