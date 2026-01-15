using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Entities;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.Repositories;

public class FocusAreaRepository : IFocusAreaRepository
{
    private readonly AppDbContext _context;

    public FocusAreaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<FocusArea>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Set<FocusArea>().OrderBy(x => x.Order).ToListAsync(ct);
    }

    public async Task<FocusArea?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.Set<FocusArea>().FindAsync([id], ct);
    }

    public async Task AddAsync(FocusArea focusArea, CancellationToken ct)
    {
        await _context.Set<FocusArea>().AddAsync(focusArea, ct);
    }

    public Task UpdateAsync(FocusArea focusArea, CancellationToken ct)
    {
        _context.Set<FocusArea>().Update(focusArea);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var focusArea = await GetByIdAsync(id, ct);
        if (focusArea != null)
        {
            _context.Set<FocusArea>().Remove(focusArea);
        }
    }
}
