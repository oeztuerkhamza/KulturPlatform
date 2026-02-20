using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.AggregateRoot;

namespace KulturPlatform.Infrastructure.ReadServices;

public class AboutUsAggregateReadService : IAboutUsAggregateReadService
{
    private readonly IAboutUsQuoteRepository _quoteRepository;
    private readonly IAboutUsWhoWeAreRepository _whoWeAreRepository;
    private readonly IAboutUsGoalsRepository _goalsRepository;
    private readonly IAboutUsVisionRepository _visionRepository;
    private readonly IAboutUsMissionRepository _missionRepository;
    private readonly IAboutUsHumanRightsRepository _humanRightsRepository;
    private readonly ICoreValueRepository _coreValueRepository;
    private readonly IFocusAreaRepository _focusAreaRepository;
    private readonly IActivityAreaRepository _activityAreaRepository;
    private readonly ITeamMemberRepository _teamMemberRepository;

    public AboutUsAggregateReadService(
        IAboutUsQuoteRepository quoteRepository,
        IAboutUsWhoWeAreRepository whoWeAreRepository,
        IAboutUsGoalsRepository goalsRepository,
        IAboutUsVisionRepository visionRepository,
        IAboutUsMissionRepository missionRepository,
        IAboutUsHumanRightsRepository humanRightsRepository,
        ICoreValueRepository coreValueRepository,
        IFocusAreaRepository focusAreaRepository,
        IActivityAreaRepository activityAreaRepository,
        ITeamMemberRepository teamMemberRepository)
    {
        _quoteRepository = quoteRepository;
        _whoWeAreRepository = whoWeAreRepository;
        _goalsRepository = goalsRepository;
        _visionRepository = visionRepository;
        _missionRepository = missionRepository;
        _humanRightsRepository = humanRightsRepository;
        _coreValueRepository = coreValueRepository;
        _focusAreaRepository = focusAreaRepository;
        _activityAreaRepository = activityAreaRepository;
        _teamMemberRepository = teamMemberRepository;
    }

    public async Task<AboutUsAggregate> GetAboutUsAggregateAsync(CancellationToken ct)
    {
        // Execute queries sequentially to avoid DbContext concurrency issues
        var quote = await _quoteRepository.GetAsync(ct);
        var whoWeAre = await _whoWeAreRepository.GetAsync(ct);
        var goals = await _goalsRepository.GetAsync(ct);
        var vision = await _visionRepository.GetAsync(ct);
        var mission = await _missionRepository.GetAsync(ct);
        var humanRights = await _humanRightsRepository.GetAsync(ct);
        var coreValues = await _coreValueRepository.GetAllAsync(ct);
        var focusAreas = await _focusAreaRepository.GetAllAsync(ct);
        var activityAreas = await _activityAreaRepository.GetAllAsync(ct);
        var teamMembers = await _teamMemberRepository.GetAllAsync(ct);

        return new AboutUsAggregate(
            quote,
            whoWeAre,
            goals,
            vision,
            mission,
            humanRights,
            coreValues,
            focusAreas,
            activityAreas,
            teamMembers
        );
    }
}
