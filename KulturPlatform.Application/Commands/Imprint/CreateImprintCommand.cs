using KulturPlatform.Application.Dtos.ImprintDto;
using MediatR;

namespace KulturPlatform.Application.Commands.Imprint
{
    public record CreateImprintCommand(
        CreateImprintDto Dto
    ) : IRequest<Guid>;
}
