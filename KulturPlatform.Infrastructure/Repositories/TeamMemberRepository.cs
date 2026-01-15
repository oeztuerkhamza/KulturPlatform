using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Entities;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.Repositories;

public class TeamMemberRepository : ITeamMemberRepository
{
    private readonly AppDbContext _context;

    public TeamMemberRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TeamMember>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Set<TeamMember>().OrderBy(x => x.Order).ToListAsync(ct);
    }

    public async Task<TeamMember?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.Set<TeamMember>().FindAsync([id], ct);
    }

    public async Task AddAsync(TeamMember teamMember, CancellationToken ct)
    {
        await _context.Set<TeamMember>().AddAsync(teamMember, ct);
    }

    public Task UpdateAsync(TeamMember teamMember, CancellationToken ct)
    {
        _context.Set<TeamMember>().Update(teamMember);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var teamMember = await GetByIdAsync(id, ct);
        if (teamMember != null)
        {
            _context.Set<TeamMember>().Remove(teamMember);
        }
    }
}
