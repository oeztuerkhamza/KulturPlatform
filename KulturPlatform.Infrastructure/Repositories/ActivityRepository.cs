using KulturPlatform.Application.Interfaces.Activity;
using KulturPlatform.Domain.Commons.AggregateRoot;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly AppDbContext _context;

        public ActivityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Activity activity, CancellationToken cancellationToken = default)
        {
            await _context.Activities.AddAsync(activity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Activity activity, CancellationToken cancellationToken = default)
        {
            _context.Activities.Update(activity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Activity activity, CancellationToken cancellationToken = default)
        {
            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Activity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Activities
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }
    }
}
