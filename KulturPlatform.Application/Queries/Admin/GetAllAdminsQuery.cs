using KulturPlatform.Application.Dtos.AdminDto;
using MediatR;

namespace KulturPlatform.Application.Queries.Admin
{
    public record GetAllAdminsQuery() : IRequest<IEnumerable<AdminDto>>;
}
