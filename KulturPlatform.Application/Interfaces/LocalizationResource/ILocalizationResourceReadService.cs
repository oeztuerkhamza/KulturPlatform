using KulturPlatform.Application.Dtos.LocalizationDto;

namespace KulturPlatform.Application.Interfaces.LocalizationResource
{
    public interface ILocalizationResourceReadService
    {
        Task<List<LocalizationResourceDto>> GetAllAsync();
        Task<LocalizationResourceDto?> GetByIdAsync(Guid id);
        Task<LocalizationResourceDto?> GetByKeyAsync(string key);
        Task<List<LocalizationResourceDto>> GetBySectionAsync(string section);
        Task<Dictionary<string, string>> GetTranslationsAsync(string languageCode);
        Task<Dictionary<string, string>> GetSectionTranslationsAsync(string section, string languageCode);
    }
}
