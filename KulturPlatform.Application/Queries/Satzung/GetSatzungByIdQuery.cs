using KulturPlatform.Application.Dtos.SatzungDto;
using MediatR;

namespace KulturPlatform.Application.Queries.Satzung
{
    public record GetSatzungByIdQuery(Guid Id) : IRequest<SatzungDto?>;
}
