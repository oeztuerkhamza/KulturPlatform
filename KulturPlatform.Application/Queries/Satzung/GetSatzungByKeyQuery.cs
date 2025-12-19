using KulturPlatform.Application.Dtos.SatzungDto;
using MediatR;

namespace KulturPlatform.Application.Queries.Satzung
{
    public record GetSatzungByKeyQuery(string Key) : IRequest<SatzungDto?>;

}
