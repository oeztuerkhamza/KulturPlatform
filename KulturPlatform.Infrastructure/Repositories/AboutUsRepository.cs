using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.Repositories
{
    public class AboutUsRepository : IAboutUsReadRepository, IAboutUsWriteRepository
    {

        private readonly AppDbContext _context;

        public AboutUsRepository(AppDbContext context)
        {
            _context = context;
        }

        // Return tracked entity (no AsNoTracking) so caller can update and SaveChanges will persist
        public async Task<AboutUs> GetAsync(CancellationToken ct)
        {
            return await _context.AboutUsEntities
                .Include(a => a.CoreValues)
                .Include(a => a.FocusAreas)
                .Include(a => a.ActivityAreas)
                .Include(a => a.TeamMembers)
                .FirstOrDefaultAsync(ct);
        }

        public async Task AddAsync(AboutUs aboutUs, CancellationToken ct)
        {
            await _context.AboutUsEntities.AddAsync(aboutUs, ct);
        }

        // Mark entity as updated in the same DbContext scope
        public async Task UpdateAsync(AboutUs aboutUs, CancellationToken ct)
        {
            // If entity is already tracked this is a no-op; otherwise attach and mark Modified
            var entry = _context.Entry(aboutUs);
            if (entry.State == EntityState.Detached)
            {
                _context.AboutUsEntities.Attach(aboutUs);
                entry = _context.Entry(aboutUs);
            }

            entry.State = EntityState.Modified;
        }

        public async Task DeleteAsync(AboutUs aboutUs, CancellationToken ct)
        {
            _context.AboutUsEntities.Remove(aboutUs);
        }
    }
}
