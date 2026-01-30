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
            var partners = await _context.Partners
                .AsNoTracking()
                .OrderBy(p => p.DisplayOrder.Value)
                .ToListAsync();

            return partners.Select(p => new PartnerDto
            {
                Id = p.Id,
                Name = p.Name.Value,
                DescriptionTr = p.DescriptionTr.Value,
                DescriptionDe = p.DescriptionDe.Value,
                LogoUrl = p.GetLogoSource(),
                WebsiteUrl = p.WebsiteUrl != null ? p.WebsiteUrl.Value : null,
                DisplayOrder = p.DisplayOrder.Value,
                IsActive = p.IsActive,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            });
        }

        public async Task<PartnerDto?> GetByIdAsync(Guid id)
        {
            var partner = await _context.Partners
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (partner == null)
                return null;

            return new PartnerDto
            {
                Id = partner.Id,
                Name = partner.Name.Value,
                DescriptionTr = partner.DescriptionTr.Value,
                DescriptionDe = partner.DescriptionDe.Value,
                LogoUrl = partner.GetLogoSource(),
                WebsiteUrl = partner.WebsiteUrl != null ? partner.WebsiteUrl.Value : null,
                DisplayOrder = partner.DisplayOrder.Value,
                IsActive = partner.IsActive,
                CreatedAt = partner.CreatedAt,
                UpdatedAt = partner.UpdatedAt
            };
        }
    }
}
