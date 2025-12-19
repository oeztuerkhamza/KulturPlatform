using MediatR;

namespace KulturPlatform.Application.Commands.Admin
{
    public record DeleteAdminCommand(Guid AdminId) : IRequest;
}
