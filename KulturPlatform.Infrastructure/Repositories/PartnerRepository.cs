using KulturPlatform.Application.Interfaces.Partner;
using KulturPlatform.Domain.Commons.Aggregates;

namespace KulturPlatform.Infrastructure.Repositories
{
    public class PartnerRepository : IPartnerRepository
    {
        private readonly AppDbContext _context;

        public PartnerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Partner?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Partners.FindAsync([id], cancellationToken);
        }

        public async Task AddAsync(Partner partner, CancellationToken cancellationToken)
        {
            await _context.Partners.AddAsync(partner, cancellationToken);
        }

        public void Update(Partner partner, CancellationToken cancellationToken)
        {
            _context.Partners.Update(partner);
        }

        public void Delete(Partner partner, CancellationToken cancellationToken)
        {
            _context.Partners.Remove(partner);
        }
    }
}
