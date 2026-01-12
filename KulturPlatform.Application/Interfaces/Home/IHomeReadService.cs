using KulturPlatform.Application.Dtos.Home;

namespace KulturPlatform.Application.Interfaces.Home
{
    public interface IHomeReadService
    {
        Task<HomeDto> GetHomePageDataAsync(string language = "tr", CancellationToken cancellationToken = default);
        Task<HeroSectionDto?> GetHeroSectionAsync(string language = "tr", CancellationToken cancellationToken = default);
        Task<List<FeatureDto>> GetFeaturesAsync(string language = "tr", CancellationToken cancellationToken = default);
        Task<CtaSectionDto?> GetCtaSectionAsync(string language = "tr", CancellationToken cancellationToken = default);
        Task<List<InstagramPostDto>> GetInstagramPostsAsync(int count = 6, CancellationToken cancellationToken = default);
    }
}
