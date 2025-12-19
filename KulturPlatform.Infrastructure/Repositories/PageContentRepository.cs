using KulturPlatform.Application.Interfaces.PageContent;
using KulturPlatform.Domain.Commons.Aggregates;

namespace KulturPlatform.Infrastructure.Repositories
{
    public class PageContentRepository : IPageContentRepository
    {
        private readonly AppDbContext _context;

        public PageContentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PageContent?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.PageContents.FindAsync([id], cancellationToken);
        }

        public async Task AddAsync(PageContent pageContent, CancellationToken cancellationToken)
        {
            await _context.PageContents.AddAsync(pageContent, cancellationToken);
        }

        public void Update(PageContent pageContent, CancellationToken cancellationToken)
        {
            _context.PageContents.Update(pageContent);
        }

        public void Delete(PageContent pageContent, CancellationToken cancellationToken)
        {
            _context.PageContents.Remove(pageContent);
        }
    }
}
