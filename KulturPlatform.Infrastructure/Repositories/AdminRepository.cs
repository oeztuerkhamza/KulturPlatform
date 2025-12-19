using KulturPlatform.Application.Interfaces.Admin;
using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;

        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Admin?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Admins.FindAsync([id], cancellationToken);
        }

        public async Task<Admin?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Admins
                .FirstOrDefaultAsync(a => a.Email.Value == email, cancellationToken);
        }

        public async Task AddAsync(Admin admin, CancellationToken cancellationToken)
        {
            await _context.Admins.AddAsync(admin, cancellationToken);
        }

        public void Update(Admin admin, CancellationToken cancellationToken)
        {
            _context.Admins.Update(admin);
        }

        public void Delete(Admin admin, CancellationToken cancellationToken)
        {
            _context.Admins.Remove(admin);
        }
    }
}
