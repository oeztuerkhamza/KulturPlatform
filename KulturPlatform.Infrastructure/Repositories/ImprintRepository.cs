using KulturPlatform.Application.Interfaces.Imprint;
using KulturPlatform.Domain.Commons.Aggregates;

namespace KulturPlatform.Infrastructure.Repositories
{
    public class ImprintRepository : IImprintRepository
    {
        private readonly AppDbContext _context;
        public ImprintRepository(AppDbContext context)
        {
            _context = context;
        }
        // Implement methods defined in IImprintRepository interface
        public async Task<Imprint?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Imprints.FindAsync([id], cancellationToken);
        }

        public async Task AddAsync(Imprint imprint, CancellationToken cancellationToken)
        {
            await _context.Imprints.AddAsync(imprint, cancellationToken);
        }

        public void Update(Imprint imprint, CancellationToken cancellationToken)
        {
            _context.Imprints.Update(imprint);
        }

        public void Delete(Imprint imprint, CancellationToken cancellationToken)
        {
            _context.Imprints.Remove(imprint);
        }
    }
}
