namespace KulturPlatform.Application.Interfaces.AboutUs;

public interface ITeamMemberRepository
{
    Task<List<Domain.Commons.Entities.TeamMember>> GetAllAsync(CancellationToken ct);
    Task<Domain.Commons.Entities.TeamMember?> GetByIdAsync(Guid id, CancellationToken ct);
    Task AddAsync(Domain.Commons.Entities.TeamMember teamMember, CancellationToken ct);
    Task UpdateAsync(Domain.Commons.Entities.TeamMember teamMember, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}
