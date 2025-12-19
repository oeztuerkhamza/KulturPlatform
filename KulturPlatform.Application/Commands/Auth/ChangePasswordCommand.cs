using MediatR;

namespace KulturPlatform.Application.Commands.Auth
{
    public record ChangePasswordCommand(
        Guid AdminId,
        string CurrentPassword,
        string NewPassword
    ) : IRequest;
}
