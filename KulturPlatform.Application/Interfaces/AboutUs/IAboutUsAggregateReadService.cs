using KulturPlatform.Domain.Commons.AggregateRoot;

namespace KulturPlatform.Application.Interfaces.AboutUs;

public interface IAboutUsAggregateReadService
{
    Task<AboutUsAggregate> GetAboutUsAggregateAsync(CancellationToken ct);
}
