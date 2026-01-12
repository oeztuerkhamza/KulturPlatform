using KulturPlatform.Application.Interfaces.Home;
using KulturPlatform.Domain.Commons.AggregateRoot;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.Repositories
{
    public class HeroSectionRepository : IHeroSectionRepository
    {
        private readonly AppDbContext _context;

        public HeroSectionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<HeroSection?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.HeroSections.FindAsync([id], cancellationToken);
        }

        public async Task<List<HeroSection>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.HeroSections.ToListAsync(cancellationToken);
        }

        public async Task<HeroSection?> GetActiveAsync(CancellationToken cancellationToken = default)
        {
            return await _context.HeroSections
                .OrderByDescending(h => h.CreatedAt)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task AddAsync(HeroSection entity, CancellationToken cancellationToken = default)
        {
            await _context.HeroSections.AddAsync(entity, cancellationToken);
        }

        public void Update(HeroSection entity, CancellationToken cancellationToken = default)
        {
            _context.HeroSections.Update(entity);
        }

        public void Delete(HeroSection entity, CancellationToken cancellationToken = default)
        {
            _context.HeroSections.Remove(entity);
        }
    }
}
