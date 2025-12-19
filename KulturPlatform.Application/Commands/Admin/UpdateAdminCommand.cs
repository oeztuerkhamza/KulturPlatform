using MediatR;

namespace KulturPlatform.Application.Commands.Admin
{
    public record UpdateAdminCommand(
        Guid Id,
        string Email,
        string Name
    ) : IRequest;
}
