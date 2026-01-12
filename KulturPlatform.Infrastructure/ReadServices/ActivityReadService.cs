using KulturPlatform.Application.Dtos.Activity;
using KulturPlatform.Application.Dtos.LocalizationDto;
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

        public async Task<IEnumerable<ActivityDto>> GetAllAsync()
        {
            return await _context.Activities
                .AsNoTracking()
                .Select(a => new ActivityDto(
                    a.Id,
                    a.TitleTr.Value,
                    a.TitleDe.Value,
                    a.DescriptionTr.Value,
                    a.DescriptionDe.Value,
                    a.DetailedContentTr != null ? a.DetailedContentTr.Value : null,
                    a.DetailedContentDe != null ? a.DetailedContentDe.Value : null,
                    a.Date.DateIso.ToString("yyyy-MM-dd"),
                    new AddressDto
                    {
                        Street = a.Address.Street,
                        HouseNo = a.Address.HouseNo,
                        ZipCode = a.Address.ZipCode,
                        City = a.Address.City,
                        State = a.Address.State,
                        Country = a.Address.Country
                    },
                    a.Category.Value,
                    a.ImageUrl != null ? a.ImageUrl.Value : null,
                    a.GalleryImages.Images.Select(img => img.Value).ToList(),
                    a.VideoUrl != null ? a.VideoUrl.Value : null,
                    a.IsActive
                ))
                .ToListAsync();
        }

        public async Task<IEnumerable<ActivityDto>> GetUpcomingAsync(CancellationToken cancellationToken)
        {
            return await _context.Activities
                .AsNoTracking()
                .Where(a => a.IsActive && a.Date.DateIso >= DateTime.UtcNow)
                .OrderBy(a => a.Date.DateIso)
                .Select(a => new ActivityDto(
                    a.Id,
                    a.TitleTr.Value,
                    a.TitleDe.Value,
                    a.DescriptionTr.Value,
                    a.DescriptionDe.Value,
                    a.DetailedContentTr != null ? a.DetailedContentTr.Value : null,
                    a.DetailedContentDe != null ? a.DetailedContentDe.Value : null,
                    a.Date.DateIso.ToString("yyyy-MM-dd"),
                    new AddressDto
                    {
                        Street = a.Address.Street,
                        HouseNo = a.Address.HouseNo,
                        ZipCode = a.Address.ZipCode,
                        City = a.Address.City,
                        State = a.Address.State,
                        Country = a.Address.Country
                    },
                    a.Category.Value,
                    a.ImageUrl != null ? a.ImageUrl.Value : null,
                    a.GalleryImages.Images.Select(img => img.Value).ToList(),
                    a.VideoUrl != null ? a.VideoUrl.Value : null,
                    a.IsActive
                ))
                .ToListAsync();
        }

        public async Task<ActivityDto?> GetByIdAsync(Guid id)
        {
            return await _context.Activities
                .AsNoTracking()
                .Where(a => a.Id == id)
                .Select(a => new ActivityDto(
                    a.Id,
                    a.TitleTr.Value,
                    a.TitleDe.Value,
                    a.DescriptionTr.Value,
                    a.DescriptionDe.Value,
                    a.DetailedContentTr != null ? a.DetailedContentTr.Value : null,
                    a.DetailedContentDe != null ? a.DetailedContentDe.Value : null,
                    a.Date.DateIso.ToString("yyyy-MM-dd"),
                    new AddressDto
                    {
                        Street = a.Address.Street,
                        HouseNo = a.Address.HouseNo,
                        ZipCode = a.Address.ZipCode,
                        City = a.Address.City,
                        State = a.Address.State,
                        Country = a.Address.Country
                    },
                    a.Category.Value,
                    a.ImageUrl != null ? a.ImageUrl.Value : null,
                    a.GalleryImages.Images.Select(img => img.Value).ToList(),
                    a.VideoUrl != null ? a.VideoUrl.Value : null,
                    a.IsActive
                ))
                .FirstOrDefaultAsync();
        }
    }
}
