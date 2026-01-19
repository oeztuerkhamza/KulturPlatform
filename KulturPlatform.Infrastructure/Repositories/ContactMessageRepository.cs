using KulturPlatform.Application.Interfaces.ContactMessages;

namespace KulturPlatform.Infrastructure.Repositories
{
    public class ContactMessageRepository : IContactMessageRepository
    {
        private readonly AppDbContext _context;

        public ContactMessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Commons.Aggregates.ContactMessage?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.ContactMessages.FindAsync([id], cancellationToken);
        }

        public async Task AddAsync(Domain.Commons.Aggregates.ContactMessage message, CancellationToken cancellationToken)
        {
            await _context.ContactMessages.AddAsync(message, cancellationToken);
        }

        public void Update(Domain.Commons.Aggregates.ContactMessage message)
        {
            _context.ContactMessages.Update(message);
        }

        public void Delete(Domain.Commons.Aggregates.ContactMessage message)
        {
            _context.ContactMessages.Remove(message);
        }
    }
}
