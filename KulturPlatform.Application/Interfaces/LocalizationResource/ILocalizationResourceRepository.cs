using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Application.Interfaces.LocalizationResource
{
    public interface ILocalizationResourceRepository : IRepository<Domain.Commons.Aggregates.LocalizationResource>
    {
        Task<Domain.Commons.Aggregates.LocalizationResource?> GetByKeyAsync(string key);
        Task<List<Domain.Commons.Aggregates.LocalizationResource>> GetBySectionAsync(string section);
        Task<List<Domain.Commons.Aggregates.LocalizationResource>> GetActiveResourcesAsync();
        Task<bool> KeyExistsAsync(string key);
    }
}
