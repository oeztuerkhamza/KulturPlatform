using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.Repositories;

public class AboutUsVisionRepository : IAboutUsVisionRepository
{
    private readonly AppDbContext _context;

    public AboutUsVisionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AboutUsVision?> GetAsync(CancellationToken ct)
    {
        return await _context.Set<AboutUsVision>().FirstOrDefaultAsync(ct);
    }

    public async Task<AboutUsVision?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.Set<AboutUsVision>().FindAsync([id], ct);
    }

    public async Task AddAsync(AboutUsVision vision, CancellationToken ct)
    {
        await _context.Set<AboutUsVision>().AddAsync(vision, ct);
    }

    public Task UpdateAsync(AboutUsVision vision, CancellationToken ct)
    {
        _context.Set<AboutUsVision>().Update(vision);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var vision = await GetByIdAsync(id, ct);
        if (vision != null)
        {
            _context.Set<AboutUsVision>().Remove(vision);
        }
    }
}
