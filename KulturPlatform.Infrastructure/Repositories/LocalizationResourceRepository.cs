using KulturPlatform.Application.Interfaces.LocalizationResource;
using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.Repositories
{
    public class LocalizationResourceRepository : ILocalizationResourceRepository
    {
        private readonly AppDbContext _context;

        public LocalizationResourceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LocalizationResource?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.LocalizationResources
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<LocalizationResource?> GetByKeyAsync(string key)
        {
            var normalizedKey = key.Trim().ToLowerInvariant();
            return await _context.LocalizationResources
                .FirstOrDefaultAsync(x => x.Key == normalizedKey);
        }

        public async Task<List<LocalizationResource>> GetBySectionAsync(string section)
        {
            var normalizedSection = section.Trim().ToLowerInvariant();
            return await _context.LocalizationResources
                .Where(x => x.Section == normalizedSection)
                .OrderBy(x => x.Key)
                .ToListAsync();
        }

        public async Task<List<LocalizationResource>> GetActiveResourcesAsync()
        {
            return await _context.LocalizationResources
                .Where(x => x.IsActive)
                .OrderBy(x => x.Section)
                .ThenBy(x => x.Key)
                .ToListAsync();
        }

        public async Task<bool> KeyExistsAsync(string key)
        {
            var normalizedKey = key.Trim().ToLowerInvariant();
            return await _context.LocalizationResources
                .AnyAsync(x => x.Key == normalizedKey);
        }

        public async Task AddAsync(LocalizationResource entity, CancellationToken cancellationToken)
        {
            await _context.LocalizationResources.AddAsync(entity, cancellationToken);
        }

        public void Update(LocalizationResource entity, CancellationToken cancellationToken)
        {
            _context.LocalizationResources.Update(entity);
        }

        public void Delete(LocalizationResource entity, CancellationToken cancellationToken)
        {
            _context.LocalizationResources.Remove(entity);
        }
    }
}
