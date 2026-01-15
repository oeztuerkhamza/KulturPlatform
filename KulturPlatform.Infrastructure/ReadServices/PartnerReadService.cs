using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Interfaces.Partner;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.ReadServices
{
    public class PartnerReadService : IPartnerReadService
    {
        private readonly AppDbContext _context;

        public PartnerReadService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PartnerDto>> GetAllAsync()
        {
            return await _context.Partners
                .AsNoTracking()
                .OrderBy(p => p.DisplayOrder.Value)
                .Select(p => new PartnerDto
                {
                    Id = p.Id,
                    Name = p.Name.Value,
                    DescriptionTr = p.DescriptionTr.Value,
                    DescriptionDe = p.DescriptionDe.Value,
                    LogoUrl = p.LogoUrl != null ? p.LogoUrl.Value : null,
                    WebsiteUrl = p.WebsiteUrl != null ? p.WebsiteUrl.Value : null,
                    DisplayOrder = p.DisplayOrder.Value,
                    IsActive = p.IsActive,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                })
                .ToListAsync();
        }

        public async Task<PartnerDto?> GetByIdAsync(Guid id)
        {
            return await _context.Partners
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new PartnerDto
                {
                    Id = p.Id,
                    Name = p.Name.Value,
                    DescriptionTr = p.DescriptionTr.Value,
                    DescriptionDe = p.DescriptionDe.Value,
                    LogoUrl = p.LogoUrl != null ? p.LogoUrl.Value : null,
                    WebsiteUrl = p.WebsiteUrl != null ? p.WebsiteUrl.Value : null,
                    DisplayOrder = p.DisplayOrder.Value,
                    IsActive = p.IsActive,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                })
                .FirstOrDefaultAsync();
        }
    }
}
