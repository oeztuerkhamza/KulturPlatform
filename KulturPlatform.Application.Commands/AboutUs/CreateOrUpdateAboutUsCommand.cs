using MediatR;
using KulturPlatform.Application.Dtos.AboutUs;

namespace KulturPlatform.Application.Commands.AboutUs
{
    public record CreateOrUpdateAboutUsCommand(string Key, UpdateAboutUsCommandDto Dto) : IRequest;
}
