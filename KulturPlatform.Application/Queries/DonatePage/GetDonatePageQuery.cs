using KulturPlatform.Application.Dtos;
using MediatR;

namespace KulturPlatform.Application.Queries.DonatePage
{
    public record GetDonatePageQuery() : IRequest<DonatePageDto?>;
}
