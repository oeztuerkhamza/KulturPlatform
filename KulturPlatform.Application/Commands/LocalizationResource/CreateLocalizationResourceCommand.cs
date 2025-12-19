using MediatR;

namespace KulturPlatform.Application.Commands.LocalizationResource
{
    public record CreateLocalizationResourceCommand(
        string Key,
        string Turkish,
        string German,
        string English,
        string Section,
        string? Description = null
    ) : IRequest<Guid>;
}
