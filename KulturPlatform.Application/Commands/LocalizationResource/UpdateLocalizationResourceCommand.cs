using MediatR;

namespace KulturPlatform.Application.Commands.LocalizationResource
{
    public record UpdateLocalizationResourceCommand(
        Guid Id,
        string Turkish,
        string German,
        string English,
        string? Description = null
    ) : IRequest;
}
