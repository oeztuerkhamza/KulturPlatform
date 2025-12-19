using KulturPlatform.Application.Interfaces.TeaEvent;
using KulturPlatform.Domain.Commons.Aggregates;

namespace KulturPlatform.Infrastructure.Repositories
{
    public sealed class TeaEventWriteRepository : ITeaEventWriteRepository
    {
        private readonly AppDbContext _context;

        public TeaEventWriteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(
            TeaEvent teaEvent,
            CancellationToken cancellationToken)
        {
            await _context.TeaEvents.AddAsync(teaEvent, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(
            TeaEvent teaEvent,
            CancellationToken cancellationToken)
        {
            _context.TeaEvents.Update(teaEvent);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(
            TeaEvent teaEvent,
            CancellationToken cancellationToken)
        {
            _context.TeaEvents.Remove(teaEvent);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}