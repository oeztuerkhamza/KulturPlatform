using KulturPlatform.Application.Interfaces.GuelenMovement;
using KulturPlatform.Domain.Commons.Aggregates;

namespace KulturPlatform.Infrastructure.Repositories
{
    public class GuelenMovementRepository : IGuelenMovementRepository
    {
        private readonly AppDbContext _context;

        public GuelenMovementRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GuelenMovement?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.GuelenMovements.FindAsync([id], cancellationToken);
        }

        public async Task AddAsync(GuelenMovement entity, CancellationToken cancellationToken)
        {
            await _context.GuelenMovements.AddAsync(entity, cancellationToken);
        }

        public void Update(GuelenMovement entity, CancellationToken cancellationToken)
        {
            _context.GuelenMovements.Update(entity);
        }

        public void Delete(GuelenMovement entity, CancellationToken cancellationToken)
        {
            _context.GuelenMovements.Remove(entity);
        }
    }
}