using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.Repositories;

public class AboutUsMissionRepository : IAboutUsMissionRepository
{
    private readonly AppDbContext _context;

    public AboutUsMissionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AboutUsMission?> GetAsync(CancellationToken ct)
    {
        return await _context.Set<AboutUsMission>().FirstOrDefaultAsync(ct);
    }

    public async Task<AboutUsMission?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.Set<AboutUsMission>().FindAsync([id], ct);
    }

    public async Task AddAsync(AboutUsMission mission, CancellationToken ct)
    {
        await _context.Set<AboutUsMission>().AddAsync(mission, ct);
    }

    public Task UpdateAsync(AboutUsMission mission, CancellationToken ct)
    {
        _context.Set<AboutUsMission>().Update(mission);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var mission = await GetByIdAsync(id, ct);
        if (mission != null)
        {
            _context.Set<AboutUsMission>().Remove(mission);
        }
    }
}
