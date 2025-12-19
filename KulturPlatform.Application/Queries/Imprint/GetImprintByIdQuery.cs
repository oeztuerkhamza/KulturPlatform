using KulturPlatform.Application.Dtos.ImprintDto;
using MediatR;

namespace KulturPlatform.Application.Queries.Imprint
{
    public record GetImprintByIdQuery(Guid Id) : IRequest<ImprintDto>;
}

