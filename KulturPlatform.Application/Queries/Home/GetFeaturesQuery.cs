using KulturPlatform.Application.Dtos.Home;
using MediatR;

namespace KulturPlatform.Application.Queries.Home
{
    public record GetFeaturesQuery(string Language = "tr") : IRequest<List<FeatureDto>>;
}
