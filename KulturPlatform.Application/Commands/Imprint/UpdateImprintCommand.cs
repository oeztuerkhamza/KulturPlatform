using KulturPlatform.Application.Dtos.ImprintDto;
using MediatR;

namespace KulturPlatform.Application.Commands.Imprint
{
    public record UpdateImprintCommand(Guid Id, UpdateImprintDto Dto) : IRequest;
}
