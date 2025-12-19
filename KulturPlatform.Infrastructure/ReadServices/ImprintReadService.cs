using KulturPlatform.Application.Interfaces.Imprint;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.ReadServices
{
    public class ImprintReadService : IImprintReadService
    {
        private readonly AppDbContext _context;

        public ImprintReadService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Commons.Aggregates.Imprint?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Imprints
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Domain.Commons.Aggregates.Imprint>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Imprints
                .AsNoTracking()
                .OrderBy(i => i.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<Domain.Commons.Aggregates.Imprint?> GetSingleAsync(CancellationToken cancellationToken = default)
        {
            // Singleton pattern - return first record
            return await _context.Imprints
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
