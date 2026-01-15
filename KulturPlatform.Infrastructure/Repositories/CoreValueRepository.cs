using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Entities;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.Repositories;

public class CoreValueRepository : ICoreValueRepository
{
    private readonly AppDbContext _context;

    public CoreValueRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CoreValue>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Set<CoreValue>().OrderBy(x => x.Order).ToListAsync(ct);
    }

    public async Task<CoreValue?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.Set<CoreValue>().FindAsync([id], ct);
    }

    public async Task AddAsync(CoreValue coreValue, CancellationToken ct)
    {
        await _context.Set<CoreValue>().AddAsync(coreValue, ct);
    }

    public Task UpdateAsync(CoreValue coreValue, CancellationToken ct)
    {
        _context.Set<CoreValue>().Update(coreValue);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var coreValue = await GetByIdAsync(id, ct);
        if (coreValue != null)
        {
            _context.Set<CoreValue>().Remove(coreValue);
        }
    }
}
