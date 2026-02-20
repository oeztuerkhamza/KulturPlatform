using AutoMapper;
using KulturPlatform.Application.Dtos.Home;
using KulturPlatform.Application.Interfaces.Activity;
using KulturPlatform.Application.Interfaces.Home;

namespace KulturPlatform.Infrastructure.ReadServices
{
    public class HomeReadService : IHomeReadService
    {
        private readonly IHeroSectionRepository _heroRepo;
        private readonly IFeatureRepository _featureRepo;
        private readonly ICtaSectionRepository _ctaRepo;
        private readonly IInstagramPostRepository _instagramRepo;
        private readonly IActivityReadService _activityReadService;
        private readonly IMapper _mapper;

        public HomeReadService(
            IHeroSectionRepository heroRepo,
            IFeatureRepository featureRepo,
            ICtaSectionRepository ctaRepo,
            IInstagramPostRepository instagramRepo,
            IActivityReadService activityReadService,
            IMapper mapper)
        {
            _heroRepo = heroRepo;
            _featureRepo = featureRepo;
            _ctaRepo = ctaRepo;
            _instagramRepo = instagramRepo;
            _activityReadService = activityReadService;
            _mapper = mapper;
        }

        public async Task<HomeDto> GetHomePageDataAsync(string language = "tr", CancellationToken cancellationToken = default)
        {
            var hero = await GetHeroSectionAsync(language, cancellationToken);
            var features = await GetFeaturesAsync(language, cancellationToken);
            var cta = await GetCtaSectionAsync(language, cancellationToken);
            var instagram = await GetInstagramPostsAsync(6, cancellationToken);
            var upcomingActivities = (await _activityReadService.GetUpcomingAsync(cancellationToken)).ToList();

            return new HomeDto(
                hero,
                features,
                cta,
                instagram,
                upcomingActivities
            );
        }

        public async Task<HeroSectionDto?> GetHeroSectionAsync(string language = "tr", CancellationToken cancellationToken = default)
        {
            var hero = await _heroRepo.GetActiveAsync(cancellationToken);
            return hero != null ? _mapper.Map<HeroSectionDto>(hero) : null;
        }

        public async Task<List<FeatureDto>> GetFeaturesAsync(string language = "tr", CancellationToken cancellationToken = default)
        {
            var features = await _featureRepo.GetAllActiveAsync(cancellationToken);
            return _mapper.Map<List<FeatureDto>>(features);
        }

        public async Task<CtaSectionDto?> GetCtaSectionAsync(string language = "tr", CancellationToken cancellationToken = default)
        {
            var cta = await _ctaRepo.GetActiveAsync(cancellationToken);
            return cta != null ? _mapper.Map<CtaSectionDto>(cta) : null;
        }

        public async Task<List<InstagramPostDto>> GetInstagramPostsAsync(int count = 6, CancellationToken cancellationToken = default)
        {
            var posts = await _instagramRepo.GetRecentAsync(count, cancellationToken);
            return _mapper.Map<List<InstagramPostDto>>(posts);
        }
    }
}
