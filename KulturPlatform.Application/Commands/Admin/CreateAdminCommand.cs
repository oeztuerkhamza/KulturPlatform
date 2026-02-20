using MediatR;

namespace KulturPlatform.Application.Commands.Admin
{
    public record CreateAdminCommand(
        string Email,
        string Password,
        string Name,
        string Role = "UserAdmin"
    ) : IRequest<Guid>;
}
