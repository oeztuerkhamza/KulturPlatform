using KulturPlatform.Application.Dtos.NewFolder;
using KulturPlatform.Application.Interfaces.ValueItem;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.ReadServices
{
    public class ValueItemReadRepository : IValueItemReadRepository
    {
        private readonly AppDbContext _context;

        public ValueItemReadRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ValueItemDetailDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.ValueItems
                .AsNoTracking()
                .Where(x => x.IsActive)
                .OrderBy(x => x.DisplayOrder.Value)
                .Select(x => new ValueItemDetailDto(
                    x.Id,
                    x.TitleTr.Value,
                    x.TitleDe.Value,
                    // Map subtitles from domain VO
                    x.SubtitleTr != null ? x.SubtitleTr.Value : string.Empty,
                    x.SubtitleDe != null ? x.SubtitleDe.Value : string.Empty,
                    x.DescriptionTr.Value,
                    x.DescriptionDe.Value,
                    x.Sections.Select(s => new SectionDto(
                        s.HeadingTr.Value,
                        s.HeadingDe.Value,
                        s.BodyTr.Value,
                        s.BodyDe.Value,
                        s.Items.Select(i => new SectionItemDto(i.TitleTr.Value, i.TitleDe.Value, i.Icon)).ToList()
                    )).ToList(),
                    // CTA buttons if present on domain (map empty if null)
                    string.Empty,
                    string.Empty,
                    x.DisplayOrder.Value,
                    x.IsActive,
                    x.CreatedAt,
                    x.UpdatedAt
                ))
                .ToListAsync(cancellationToken);
        }

        public async Task<ValueItemDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _context.ValueItems
                .AsNoTracking()
                .Include(x => x.Sections)
                    .ThenInclude(s => s.Items)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive, cancellationToken);

            if (entity == null)
                return null;

            var sections = entity.Sections.Select(s => new SectionDto(
                s.HeadingTr.Value,
                s.HeadingDe.Value,
                s.BodyTr.Value,
                s.BodyDe.Value,
                s.Items.Select(i => new SectionItemDto(i.TitleTr.Value, i.TitleDe.Value, i.Icon)).ToList()
            )).ToList();

            return new ValueItemDetailDto(
                entity.Id,
                entity.TitleTr.Value,
                entity.TitleDe.Value,
                // Map subtitles
                entity.SubtitleTr != null ? entity.SubtitleTr.Value : string.Empty,
                entity.SubtitleDe != null ? entity.SubtitleDe.Value : string.Empty,
                entity.DescriptionTr.Value,
                entity.DescriptionDe.Value,
                sections,
                // CTA buttons if stored on entity (not present currently) - keep empty
                string.Empty,
                string.Empty,
                entity.DisplayOrder.Value,
                entity.IsActive,
                entity.CreatedAt,
                entity.UpdatedAt
            );
        }
    }
}
