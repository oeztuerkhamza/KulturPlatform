using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Interfaces.GuelenMovement;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.ReadServices
{
    public class GuelenMovementReadService : IGuelenMovementReadService
    {
        private readonly AppDbContext _context;

        public GuelenMovementReadService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GuelenMovementDto>> GetAllAsync()
        {
            return await _context.GuelenMovements
                .AsNoTracking()
                .Select(a => new GuelenMovementDto
                {
                    Id = a.Id,
                    TitleTr = a.TitleTurkish.Value,
                    TitleDe = a.TitleGerman.Value,
                    ContentTr = a.ContentTurkish,
                    ContentDe = a.ContentGerman,
                    ImageUrl = a.ImageUrl.Value
                })
                .ToListAsync();
        }

        public async Task<GuelenMovementDto?> GetByIdAsync(Guid id)
        {
            return await _context.GuelenMovements
                .AsNoTracking()
                .Where(a => a.Id == id)
                .Select(a => new GuelenMovementDto
                {
                    Id = a.Id,
                    TitleTr = a.TitleTurkish.Value,
                    TitleDe = a.TitleGerman.Value,
                    ContentTr = a.ContentTurkish,
                    ContentDe = a.ContentGerman,
                    ImageUrl = a.ImageUrl.Value
                })
                .FirstOrDefaultAsync();
        }
    }
}