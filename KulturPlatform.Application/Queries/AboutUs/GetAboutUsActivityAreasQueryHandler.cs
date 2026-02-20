using AutoMapper;
using KulturPlatform.Application.Dtos.AboutUs;
using KulturPlatform.Application.Interfaces.AboutUs;
using MediatR;

namespace KulturPlatform.Application.Queries.AboutUs;

public class GetAboutUsActivityAreasQueryHandler : IRequestHandler<GetAboutUsActivityAreasQuery, List<ActivityAreaDto>>
{
    private readonly IActivityAreaRepository _activityAreaRepository;
    private readonly IMapper _mapper;

    public GetAboutUsActivityAreasQueryHandler(IActivityAreaRepository activityAreaRepository, IMapper mapper)
    {
        _activityAreaRepository = activityAreaRepository;
        _mapper = mapper;
    }

    public async Task<List<ActivityAreaDto>> Handle(GetAboutUsActivityAreasQuery request, CancellationToken cancellationToken)
    {
        var activityAreas = await _activityAreaRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<ActivityAreaDto>>(activityAreas);
    }
}
