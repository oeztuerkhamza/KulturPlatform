using MediatR;

namespace KulturPlatform.Application.Commands.Admin
{
    public record UpdateAdminRoleCommand(
        Guid AdminId,
        string Role
    ) : IRequest;
}
