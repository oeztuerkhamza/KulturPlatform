using AutoMapper;
using KulturPlatform.Application.Dtos.LocalizationDto;
using KulturPlatform.Application.Interfaces.LocalizationResource;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.ReadServices
{
    public class LocalizationResourceReadService : ILocalizationResourceReadService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public LocalizationResourceReadService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<LocalizationResourceDto>> GetAllAsync()
        {
            var resources = await _context.LocalizationResources
                .OrderBy(x => x.Section)
                .ThenBy(x => x.Key)
                .ToListAsync();

            return _mapper.Map<List<LocalizationResourceDto>>(resources);
        }

        public async Task<LocalizationResourceDto?> GetByIdAsync(Guid id)
        {
            var resource = await _context.LocalizationResources
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<LocalizationResourceDto>(resource);
        }

        public async Task<LocalizationResourceDto?> GetByKeyAsync(string key)
        {
            var normalizedKey = key.Trim().ToLowerInvariant();
            var resource = await _context.LocalizationResources
                .FirstOrDefaultAsync(x => x.Key == normalizedKey);

            return _mapper.Map<LocalizationResourceDto>(resource);
        }

        public async Task<List<LocalizationResourceDto>> GetBySectionAsync(string section)
        {
            var normalizedSection = section.Trim().ToLowerInvariant();
            var resources = await _context.LocalizationResources
                .Where(x => x.Section == normalizedSection)
                .OrderBy(x => x.Key)
                .ToListAsync();

            return _mapper.Map<List<LocalizationResourceDto>>(resources);
        }

        public async Task<Dictionary<string, string>> GetTranslationsAsync(string languageCode)
        {
            var resources = await _context.LocalizationResources
                .Where(x => x.IsActive)
                .ToListAsync();

            return resources.ToDictionary(
                x => x.Key,
                x => x.GetTranslation(languageCode)
            );
        }

        public async Task<Dictionary<string, string>> GetSectionTranslationsAsync(string section, string languageCode)
        {
            var normalizedSection = section.Trim().ToLowerInvariant();
            var resources = await _context.LocalizationResources
                .Where(x => x.Section == normalizedSection && x.IsActive)
                .ToListAsync();

            return resources.ToDictionary(
                x => x.Key,
                x => x.GetTranslation(languageCode)
            );
        }
    }
}
