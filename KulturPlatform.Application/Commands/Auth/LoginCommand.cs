using KulturPlatform.Application.Dtos.AuthDto;
using MediatR;

namespace KulturPlatform.Application.Commands.Auth
{
    public record LoginCommand(
        string Email,
        string Password
    ) : IRequest<LoginResponseDto>;
}
