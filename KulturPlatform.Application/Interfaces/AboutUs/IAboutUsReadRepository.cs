namespace KulturPlatform.Application.Interfaces.AboutUs
{
    public interface IAboutUsReadRepository
    {
        Task<Domain.Commons.Aggregates.AboutUs> GetAsync(CancellationToken ct);
    }

}
