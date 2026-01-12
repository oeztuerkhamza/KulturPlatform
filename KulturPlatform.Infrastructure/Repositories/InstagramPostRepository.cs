using KulturPlatform.Application.Interfaces.Home;
using KulturPlatform.Domain.Commons.AggregateRoot;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.Repositories
{
    public class InstagramPostRepository : IInstagramPostRepository
    {
        private readonly AppDbContext _context;

        public InstagramPostRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<InstagramPost?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.InstagramPosts.FindAsync([id], cancellationToken);
        }

        public async Task<List<InstagramPost>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.InstagramPosts.ToListAsync(cancellationToken);
        }

        public async Task<List<InstagramPost>> GetRecentAsync(int count = 6, CancellationToken cancellationToken = default)
        {
            return await _context.InstagramPosts
                .OrderByDescending(i => i.CreatedAt)
                .Take(count)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(InstagramPost entity, CancellationToken cancellationToken = default)
        {
            await _context.InstagramPosts.AddAsync(entity, cancellationToken);
        }

        public void Update(InstagramPost entity, CancellationToken cancellationToken = default)
        {
            _context.InstagramPosts.Update(entity);
        }

        public void Delete(InstagramPost entity, CancellationToken cancellationToken = default)
        {
            _context.InstagramPosts.Remove(entity);
        }
    }
}
