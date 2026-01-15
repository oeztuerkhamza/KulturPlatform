using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Entities;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.Repositories;

public class ActivityAreaRepository : IActivityAreaRepository
{
    private readonly AppDbContext _context;

    public ActivityAreaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ActivityArea>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Set<ActivityArea>().OrderBy(x => x.Order).ToListAsync(ct);
    }

    public async Task<ActivityArea?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.Set<ActivityArea>().FindAsync([id], ct);
    }

    public async Task AddAsync(ActivityArea activityArea, CancellationToken ct)
    {
        await _context.Set<ActivityArea>().AddAsync(activityArea, ct);
    }

    public Task UpdateAsync(ActivityArea activityArea, CancellationToken ct)
    {
        _context.Set<ActivityArea>().Update(activityArea);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var activityArea = await GetByIdAsync(id, ct);
        if (activityArea != null)
        {
            _context.Set<ActivityArea>().Remove(activityArea);
        }
    }
}
