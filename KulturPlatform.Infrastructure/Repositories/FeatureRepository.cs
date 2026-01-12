using KulturPlatform.Application.Interfaces.Home;
using KulturPlatform.Domain.Commons.AggregateRoot;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.Repositories
{
    public class FeatureRepository : IFeatureRepository
    {
        private readonly AppDbContext _context;

        public FeatureRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Feature?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Features.FindAsync([id], cancellationToken);
        }

        public async Task<List<Feature>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Features.ToListAsync(cancellationToken);
        }

        public async Task<List<Feature>> GetAllActiveAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Features
                .OrderBy(f => f.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Feature entity, CancellationToken cancellationToken = default)
        {
            await _context.Features.AddAsync(entity, cancellationToken);
        }

        public void Update(Feature entity, CancellationToken cancellationToken = default)
        {
            _context.Features.Update(entity);
        }

        public void Delete(Feature entity, CancellationToken cancellationToken = default)
        {
            _context.Features.Remove(entity);
        }
    }
}
