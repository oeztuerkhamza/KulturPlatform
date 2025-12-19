using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Dtos.SatzungDto;
using MediatR;

namespace KulturPlatform.Application.Queries.Satzung
{
    public record GetAllSatzungQuery() : IRequest<IEnumerable<SatzungDto>>;
}
