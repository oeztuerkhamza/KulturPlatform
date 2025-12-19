using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Interfaces.Activity;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.ReadServices
{
    public class ActivityReadService : IActivityReadService
    {
        private readonly AppDbContext _context;
        public ActivityReadService(AppDbContext context)
        {
            _context = context;
        }
        // Implement read methods for Activity entity here
        public async Task<IEnumerable<ActivityDto>> GetAllAsync()
        {
            return await _context.Activities
                .AsNoTracking()
                .Select(a => new ActivityDto
                {
                    Id = a.Id,
                    TitleTr = a.TitleTr.Value,
                    TitleDe = a.TitleDe.Value,
                    IsActive = a.IsActive
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ActivityDto>> GetUpcomingAsync()
        {
            return await _context.Activities
                .AsNoTracking()
                .Where(a => a.IsActive && a.DateTr.DateISO >= DateTime.UtcNow)
                .OrderBy(a => a.DateTr.DateISO)
                .Select(a => new ActivityDto
                {
                    Id = a.Id,
                    TitleTr = a.TitleTr.Value,
                    TitleDe = a.TitleDe.Value,
                    DateTr = a.DateTr.DateISO,
                    DateDe = a.DateDe.DateISO,
                    IsActive = a.IsActive
                })
                .ToListAsync();
        }

        public async Task<ActivityDto?> GetByIdAsync(Guid id)
        {
            return await _context.Activities
                .AsNoTracking()
                .Where(a => a.Id == id)
                .Select(a => new ActivityDto
                {
                    Id = a.Id,
                    TitleTr = a.TitleTr.Value,
                    TitleDe = a.TitleDe.Value,
                    DescriptionTr = a.DescriptionTr.Value,
                    DescriptionDe = a.DescriptionDe.Value,
                    DetailedContentTr = a.DetailedContentTr,
                    DetailedContentDe = a.DetailedContentDe,
                    DateTr = a.DateTr.DateISO,
                    DateDe = a.DateDe.DateISO,
                    Location = a.Address.Street,
                    Category = a.Category.Value,
                    ImageUrl = a.ImageUrl != null ? a.ImageUrl.Value : null,
                    GalleryImages = a.GalleryImages.Images.Select(img => img.Value).ToList(),
                    VideoUrl = a.VideoUrl != null ? a.VideoUrl.Value : null,
                    IsActive = a.IsActive,
                    CreatedAt = a.CreatedAt,
                    UpdatedAt = a.UpdatedAt
                })
                .FirstOrDefaultAsync();
        }
    }
}
