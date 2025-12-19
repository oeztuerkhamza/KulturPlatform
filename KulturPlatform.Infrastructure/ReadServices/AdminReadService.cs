using KulturPlatform.Application.Interfaces.Admin;
using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.ReadServices
{
    public class AdminReadService : IAdminReadService
    {
        private readonly AppDbContext _context;

        public AdminReadService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Admin?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Admins
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Admin>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Admins
                .AsNoTracking()
                .OrderBy(a => a.Email.Value)
                .ToListAsync(cancellationToken);
        }

        public async Task<Admin?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Admins
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Email.Value == email, cancellationToken);
        }
    }
}
