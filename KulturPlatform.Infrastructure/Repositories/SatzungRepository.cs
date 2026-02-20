using KulturPlatform.Application.Interfaces.Satzung;
using KulturPlatform.Domain.Commons.Aggregates;

namespace KulturPlatform.Infrastructure.Repositories
{
    public class SatzungRepository : ISatzungRepository
    {
        private readonly AppDbContext _context;

        public SatzungRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Satzung?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Satzungen.FindAsync([id], cancellationToken);
        }

        public async Task AddAsync(Satzung entity, CancellationToken cancellationToken)
        {
            await _context.Satzungen.AddAsync(entity, cancellationToken);
        }

        public void Update(Satzung entity, CancellationToken cancellationToken)
        {
            _context.Satzungen.Update(entity);
        }

        public void Delete(Satzung entity, CancellationToken cancellationToken)
        {
            _context.Satzungen.Remove(entity);
        }
    }
}