using KulturPlatform.Application.Interfaces.Activity;
using KulturPlatform.Domain.Commons.AggregateRoot;

namespace KulturPlatform.Infrastructure.Repositories
{
    public class ActivityRepository(AppDbContext context) : IActivityRepository
    {
        private readonly AppDbContext _context = context;

        // Implement repository methods here

        public async Task<Activity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Activities.FindAsync(id);
        }

        public async Task AddAsync(Activity activity, CancellationToken cancellationToken)
        {
            await _context.Activities.AddAsync(activity, cancellationToken);
        }

        public void Update(Activity activity, CancellationToken cancellationToken)
        {
            _context.Activities.Update(activity);
        }

        public void Delete(Activity activity, CancellationToken cancellationToken)
        {
            _context.Activities.Remove(activity);
        }
    }
}
