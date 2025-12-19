using KulturPlatform.Application.Dtos;
using MediatR;

namespace KulturPlatform.Application.Queries.Partner
{
    public record GetAllPartnersQuery() : IRequest<IEnumerable<PartnerDto>>;
}
