using AutoMapper;
using KulturPlatform.Application.Dtos.AboutUs;
using KulturPlatform.Application.Interfaces.AboutUs;
using MediatR;

namespace KulturPlatform.Application.Queries.AboutUs;

public class GetAboutUsValuesQueryHandler : IRequestHandler<GetAboutUsValuesQuery, AboutUsValuesDto>
{
    private readonly IAboutUsVisionRepository _visionRepository;
    private readonly IAboutUsMissionRepository _missionRepository;
    private readonly ICoreValueRepository _coreValueRepository;
    private readonly IMapper _mapper;

    public GetAboutUsValuesQueryHandler(
        IAboutUsVisionRepository visionRepository,
        IAboutUsMissionRepository missionRepository,
        ICoreValueRepository coreValueRepository,
        IMapper mapper)
    {
        _visionRepository = visionRepository;
        _missionRepository = missionRepository;
        _coreValueRepository = coreValueRepository;
        _mapper = mapper;
    }

    public async Task<AboutUsValuesDto> Handle(GetAboutUsValuesQuery request, CancellationToken cancellationToken)
    {
        var vision = await _visionRepository.GetAsync(cancellationToken);
        var mission = await _missionRepository.GetAsync(cancellationToken);
        var coreValues = await _coreValueRepository.GetAllAsync(cancellationToken);

        return new AboutUsValuesDto
        {
            Vision = vision != null ? _mapper.Map<AboutUsVisionDto>(vision) : null,
            Mission = mission != null ? _mapper.Map<AboutUsMissionDto>(mission) : null,
            CoreValues = _mapper.Map<List<CoreValueDto>>(coreValues)
        };
    }
}
