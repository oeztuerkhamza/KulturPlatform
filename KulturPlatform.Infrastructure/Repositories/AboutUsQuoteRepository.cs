using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.Repositories;

public class AboutUsQuoteRepository : IAboutUsQuoteRepository
{
    private readonly AppDbContext _context;

    public AboutUsQuoteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AboutUsQuote?> GetAsync(CancellationToken ct)
    {
        return await _context.Set<AboutUsQuote>().FirstOrDefaultAsync(ct);
    }

    public async Task<AboutUsQuote?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.Set<AboutUsQuote>().FindAsync([id], ct);
    }

    public async Task AddAsync(AboutUsQuote quote, CancellationToken ct)
    {
        await _context.Set<AboutUsQuote>().AddAsync(quote, ct);
    }

    public Task UpdateAsync(AboutUsQuote quote, CancellationToken ct)
    {
        _context.Set<AboutUsQuote>().Update(quote);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var quote = await GetByIdAsync(id, ct);
        if (quote != null)
        {
            _context.Set<AboutUsQuote>().Remove(quote);
        }
    }
}
