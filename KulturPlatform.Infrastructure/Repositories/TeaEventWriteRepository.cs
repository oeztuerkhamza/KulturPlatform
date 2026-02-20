using KulturPlatform.Application.Interfaces.TeaEvent;
using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.Repositories
{
    public sealed class TeaEventWriteRepository : ITeaEventWriteRepository
    {
        private readonly AppDbContext _context;

        public TeaEventWriteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TeaEvent?> GetByIdForUpdateAsync(Guid id, CancellationToken cancellationToken)
        {
            // ✅ Tracking ile entity al (AsNoTracking YOK!)
            return await _context.TeaEvents
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task AddAsync(
            TeaEvent teaEvent,
            CancellationToken cancellationToken)
        {
            await _context.TeaEvents.AddAsync(teaEvent, cancellationToken);
        }

        public async Task UpdateAsync(
            TeaEvent teaEvent,
            CancellationToken cancellationToken)
        {
            _context.TeaEvents.Update(teaEvent);
        }

        public async Task DeleteAsync(
            TeaEvent teaEvent,
            CancellationToken cancellationToken)
        {
            _context.TeaEvents.Remove(teaEvent);
        }
    }
}