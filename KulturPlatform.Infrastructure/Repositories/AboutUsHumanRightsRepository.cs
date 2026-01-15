using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.Repositories;

public class AboutUsHumanRightsRepository : IAboutUsHumanRightsRepository
{
    private readonly AppDbContext _context;

    public AboutUsHumanRightsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AboutUsHumanRights?> GetAsync(CancellationToken ct)
    {
        return await _context.Set<AboutUsHumanRights>().FirstOrDefaultAsync(ct);
    }

    public async Task<AboutUsHumanRights?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.Set<AboutUsHumanRights>().FindAsync([id], ct);
    }

    public async Task AddAsync(AboutUsHumanRights humanRights, CancellationToken ct)
    {
        await _context.Set<AboutUsHumanRights>().AddAsync(humanRights, ct);
    }

    public Task UpdateAsync(AboutUsHumanRights humanRights, CancellationToken ct)
    {
        _context.Set<AboutUsHumanRights>().Update(humanRights);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var humanRights = await GetByIdAsync(id, ct);
        if (humanRights != null)
        {
            _context.Set<AboutUsHumanRights>().Remove(humanRights);
        }
    }
}
