using KulturPlatform.Application.Dtos.AboutUs;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs
{
    public record CreateOrUpdateAboutUsCommand(AboutUsDto Dto)
        : IRequest<Unit>;
}
