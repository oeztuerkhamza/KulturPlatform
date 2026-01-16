using KulturPlatform.Application.Dtos.Home;
using MediatR;

namespace KulturPlatform.Application.Queries.Home
{
    public record GetCtaSectionQuery(string Language = "tr") : IRequest<CtaSectionDto?>;
}
