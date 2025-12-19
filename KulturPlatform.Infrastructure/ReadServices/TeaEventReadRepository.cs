using KulturPlatform.Application.Interfaces.TeaEvent;
using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.ReadServices
{
    public sealed class TeaEventReadRepository : ITeaEventReadRepository
    {
        private readonly AppDbContext _context;

        public TeaEventReadRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TeaEvent?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            return await _context.TeaEvents
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<TeaEvent?> GetActiveAsync(
            CancellationToken cancellationToken)
        {
            return await _context.TeaEvents
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IsActive, cancellationToken);
        }

        public async Task<IReadOnlyList<TeaEvent>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return await _context.TeaEvents
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);
        }
    }
}