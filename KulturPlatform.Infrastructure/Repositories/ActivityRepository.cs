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
            // Load the existing tracked entity from database
            var existingActivity = await _context.Activities
                .FirstOrDefaultAsync(a => a.Id == activity.Id, cancellationToken);

            if (existingActivity == null)
                throw new KeyNotFoundException($"Activity with Id {activity.Id} not found.");

            // ✅ Clear and re-populate gallery images instead of replacing the collection
            if (existingActivity.GalleryImages?.Images != null)
            {
                existingActivity.GalleryImages.Images.Clear();
                
                // Add new images if provided
                if (activity.GalleryImages?.Images != null && activity.GalleryImages.Images.Any())
                {
                    foreach (var img in activity.GalleryImages.Images)
                    {
                        existingActivity.GalleryImages.Images.Add(img);
                    }
                }
            }

            // Update the existing tracked entity using the Update method from domain
            // BUT pass null for galleryImages since we've already handled it above
            existingActivity.Update(
                activity.TitleTr,
                activity.TitleDe,
                activity.DescriptionTr,
                activity.DescriptionDe,
                activity.Date,
                activity.Address,
                activity.Category,
                activity.ImageUrl,
                activity.ImageData,
                null, // ✅ Don't pass gallery images here
                activity.VideoUrl,
                activity.IsActive,
                activity.DetailedContentTr,
                activity.DetailedContentDe
            );

            // Save changes - EF Core will track all changes automatically
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
