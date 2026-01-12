using KulturPlatform.Application.Interfaces.Home;
using KulturPlatform.Domain.Commons.AggregateRoot;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.Repositories
{
    public class CtaSectionRepository : ICtaSectionRepository
    {
        private readonly AppDbContext _context;

        public CtaSectionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CtaSection?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.CtaSections.FindAsync([id], cancellationToken);
        }

        public async Task<List<CtaSection>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.CtaSections.ToListAsync(cancellationToken);
        }

        public async Task<CtaSection?> GetActiveAsync(CancellationToken cancellationToken = default)
        {
            return await _context.CtaSections
                .OrderByDescending(c => c.CreatedAt)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task AddAsync(CtaSection entity, CancellationToken cancellationToken = default)
        {
            await _context.CtaSections.AddAsync(entity, cancellationToken);
        }

        public void Update(CtaSection entity, CancellationToken cancellationToken = default)
        {
            _context.CtaSections.Update(entity);
        }

        public void Delete(CtaSection entity, CancellationToken cancellationToken = default)
        {
            _context.CtaSections.Remove(entity);
        }
    }
}
