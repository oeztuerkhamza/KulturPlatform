using KulturPlatform.Application.Dtos.AdminDto;
using MediatR;

namespace KulturPlatform.Application.Queries.Admin
{
    public record GetAdminByIdQuery(Guid Id) : IRequest<AdminDto?>;
}
