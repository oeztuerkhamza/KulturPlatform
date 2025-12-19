namespace KulturPlatform.Application.Interfaces.AboutUs
{
    public interface IAboutUsWriteRepository
    {
        Task<Domain.Commons.Aggregates.AboutUs> GetAsync(CancellationToken ct);
        Task AddAsync(Domain.Commons.Aggregates.AboutUs aboutUs, CancellationToken ct);
        Task UpdateAsync(Domain.Commons.Aggregates.AboutUs aboutUs, CancellationToken ct);
        Task DeleteAsync(Domain.Commons.Aggregates.AboutUs aboutUs, CancellationToken ct);
    }

}
