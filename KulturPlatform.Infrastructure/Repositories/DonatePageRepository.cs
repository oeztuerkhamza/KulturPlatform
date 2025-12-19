using KulturPlatform.Application.Interfaces.DonatePage;
using KulturPlatform.Domain.Commons.Aggregates;

namespace KulturPlatform.Infrastructure.Repositories
{
    public class DonatePageRepository : IDonatePageRepository
    {
        private readonly AppDbContext _context;

        public DonatePageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DonatePage?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.DonatePages.FindAsync([id], cancellationToken);
        }

        public async Task AddAsync(DonatePage entity, CancellationToken cancellationToken)
        {
            await _context.DonatePages.AddAsync(entity, cancellationToken);
        }

        public void Update(DonatePage entity, CancellationToken cancellationToken)
        {
            _context.DonatePages.Update(entity);
        }

        public void Delete(DonatePage entity, CancellationToken cancellationToken)
        {
            _context.DonatePages.Remove(entity);
        }
    }
}