using KulturPlatform.Application.Interfaces.ContactInfo;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.ReadServices
{
    public class ContactInfoReadService : IContactInfoReadService
    {
        private readonly AppDbContext _dbContext;

        public ContactInfoReadService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Commons.Aggregates.ContactInfo?> GetContactInfoAsync(CancellationToken cancellationToken = default)
        {
            // ContactInfo should be singleton - get the first record
            return await _dbContext.ContactInfos
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Domain.Commons.Aggregates.ContactInfo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.ContactInfos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Domain.Commons.Aggregates.ContactInfo>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.ContactInfos
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
