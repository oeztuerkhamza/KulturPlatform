using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Interfaces.PageContent;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.ReadServices
{
    public class PageContentReadService : IPageContentReadService
    {
        private readonly AppDbContext _context;

        public PageContentReadService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PageContentDto>> GetAllAsync()
        {
            return await _context.PageContents
                .AsNoTracking()
                .Select(p => new PageContentDto
                {
                    Id = p.Id,
                    PageName = p.PageName.Value,
                    SectionKey = p.SectionKey.Value,
                    ContentTr = p.ContentTr.Value,
                    ContentDe = p.ContentDe.Value,
                    IsActive = p.IsActive,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                })
                .ToListAsync();
        }

        public async Task<PageContentDto?> GetByIdAsync(Guid id)
        {
            return await _context.PageContents
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new PageContentDto
                {
                    Id = p.Id,
                    PageName = p.PageName.Value,
                    SectionKey = p.SectionKey.Value,
                    ContentTr = p.ContentTr.Value,
                    ContentDe = p.ContentDe.Value,
                    IsActive = p.IsActive,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PageContentDto>> GetByPageNameAsync(string pageName)
        {
            return await _context.PageContents
                .AsNoTracking()
                .Where(p => p.PageName.Value == pageName && p.IsActive)
                .Select(p => new PageContentDto
                {
                    Id = p.Id,
                    PageName = p.PageName.Value,
                    SectionKey = p.SectionKey.Value,
                    ContentTr = p.ContentTr.Value,
                    ContentDe = p.ContentDe.Value,
                    IsActive = p.IsActive,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                })
                .ToListAsync();
        }
    }
}
