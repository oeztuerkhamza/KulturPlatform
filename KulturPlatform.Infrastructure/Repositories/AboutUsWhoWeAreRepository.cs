using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.Repositories;

public class AboutUsWhoWeAreRepository : IAboutUsWhoWeAreRepository
{
    private readonly AppDbContext _context;

    public AboutUsWhoWeAreRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AboutUsWhoWeAre?> GetAsync(CancellationToken ct)
    {
        return await _context.Set<AboutUsWhoWeAre>().FirstOrDefaultAsync(ct);
    }

    public async Task<AboutUsWhoWeAre?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.Set<AboutUsWhoWeAre>().FindAsync([id], ct);
    }

    public async Task AddAsync(AboutUsWhoWeAre whoWeAre, CancellationToken ct)
    {
        await _context.Set<AboutUsWhoWeAre>().AddAsync(whoWeAre, ct);
    }

    public Task UpdateAsync(AboutUsWhoWeAre whoWeAre, CancellationToken ct)
    {
        _context.Set<AboutUsWhoWeAre>().Update(whoWeAre);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var whoWeAre = await GetByIdAsync(id, ct);
        if (whoWeAre != null)
        {
            _context.Set<AboutUsWhoWeAre>().Remove(whoWeAre);
        }
    }
}
