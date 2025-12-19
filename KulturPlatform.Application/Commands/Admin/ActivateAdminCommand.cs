using MediatR;

namespace KulturPlatform.Application.Commands.Admin
{
    public record ActivateAdminCommand(Guid AdminId) : IRequest;
}
