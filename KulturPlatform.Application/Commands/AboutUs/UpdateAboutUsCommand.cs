using KulturPlatform.Application.Dtos.AboutUs;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs
{
    public record UpdateAboutUsCommand(AboutUsDto Model) : IRequest<Unit>;

}
