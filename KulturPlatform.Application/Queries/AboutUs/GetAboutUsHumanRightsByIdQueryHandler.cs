using AutoMapper;
using KulturPlatform.Application.Dtos.AboutUs;
using KulturPlatform.Application.Interfaces.AboutUs;
using MediatR;

namespace KulturPlatform.Application.Queries.AboutUs;

public class GetAboutUsHumanRightsByIdQueryHandler : IRequestHandler<GetAboutUsHumanRightsByIdQuery, AboutUsHumanRightsDto?>
{
    private readonly IAboutUsHumanRightsRepository _repository;
    private readonly IMapper _mapper;

    public GetAboutUsHumanRightsByIdQueryHandler(IAboutUsHumanRightsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<AboutUsHumanRightsDto?> Handle(GetAboutUsHumanRightsByIdQuery request, CancellationToken cancellationToken)
    {
        var humanRights = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return humanRights != null ? _mapper.Map<AboutUsHumanRightsDto>(humanRights) : null;
    }
}
