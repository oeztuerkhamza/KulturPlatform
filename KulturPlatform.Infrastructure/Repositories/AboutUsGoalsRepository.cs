using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.Repositories;

public class AboutUsGoalsRepository : IAboutUsGoalsRepository
{
    private readonly AppDbContext _context;

    public AboutUsGoalsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AboutUsGoals?> GetAsync(CancellationToken ct)
    {
        return await _context.Set<AboutUsGoals>().FirstOrDefaultAsync(ct);
    }

    public async Task<AboutUsGoals?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.Set<AboutUsGoals>().FindAsync([id], ct);
    }

    public async Task AddAsync(AboutUsGoals goals, CancellationToken ct)
    {
        await _context.Set<AboutUsGoals>().AddAsync(goals, ct);
    }

    public Task UpdateAsync(AboutUsGoals goals, CancellationToken ct)
    {
        _context.Set<AboutUsGoals>().Update(goals);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var goals = await GetByIdAsync(id, ct);
        if (goals != null)
        {
            _context.Set<AboutUsGoals>().Remove(goals);
        }
    }
}
