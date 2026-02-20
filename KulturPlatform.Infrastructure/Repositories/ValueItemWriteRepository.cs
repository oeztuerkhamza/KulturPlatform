using KulturPlatform.Application.Interfaces.ValueItem;
using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.Repositories
{
    public class ValueItemWriteRepository : IValueItemWriteRepository
    {
        private readonly AppDbContext _context;

        public ValueItemWriteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ValueItem valueItem, CancellationToken cancellationToken)
        {
            await _context.ValueItems.AddAsync(valueItem, cancellationToken);
        }

        public async Task UpdateAsync(ValueItem valueItem, CancellationToken cancellationToken)
        {
            _context.ValueItems.Update(valueItem);
        }

        public async Task<ValueItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.ValueItems
                .Include(x => x.Sections)            // Sections'ları da yükle
                .ThenInclude(s => s.Items)       // SectionItem’ları da yükle
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        public void Delete(ValueItem valueItem)
        {
            _context.ValueItems.Remove(valueItem);
        }    }

}
