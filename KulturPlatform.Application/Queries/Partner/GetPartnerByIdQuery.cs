using KulturPlatform.Application.Dtos;
using MediatR;

namespace KulturPlatform.Application.Queries.Partner
{
    public record GetPartnerByIdQuery(Guid Id) : IRequest<PartnerDto?>;
}
