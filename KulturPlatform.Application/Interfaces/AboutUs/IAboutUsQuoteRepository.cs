namespace KulturPlatform.Application.Interfaces.AboutUs;

public interface IAboutUsQuoteRepository
{
    Task<Domain.Commons.Aggregates.AboutUsQuote?> GetAsync(CancellationToken ct);
    Task<Domain.Commons.Aggregates.AboutUsQuote?> GetByIdAsync(Guid id, CancellationToken ct);
    Task AddAsync(Domain.Commons.Aggregates.AboutUsQuote quote, CancellationToken ct);
    Task UpdateAsync(Domain.Commons.Aggregates.AboutUsQuote quote, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}
