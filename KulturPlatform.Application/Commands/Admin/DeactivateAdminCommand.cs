using MediatR;

namespace KulturPlatform.Application.Commands.Admin
{
    public record DeactivateAdminCommand(Guid AdminId) : IRequest;
}
