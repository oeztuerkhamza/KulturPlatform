using KulturPlatform.Application.Interfaces.ContactInfo;
using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.Repositories
{
    public class ContactInfoRepository : IContactInfoRepository
    {
        private readonly AppDbContext _context;

        public ContactInfoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ContactInfo?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.ContactInfos.FindAsync(id);
        }

        public async Task AddAsync(ContactInfo contactInfo, CancellationToken cancellationToken)
        {
            await _context.ContactInfos.AddAsync(contactInfo, cancellationToken);
        }

        public void Update(ContactInfo contactInfo, CancellationToken cancellationToken)
        {
            _context.Attach(contactInfo);
            _context.Entry(contactInfo).State = EntityState.Modified;
        }

        public void Delete(ContactInfo contactInfo, CancellationToken cancellationToken)
        {
            _context.ContactInfos.Remove(contactInfo);
        }
    }
}