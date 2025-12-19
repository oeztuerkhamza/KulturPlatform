using MediatR;

namespace KulturPlatform.Application.Commands.LocalizationResource
{
    public record ActivateLocalizationResourceCommand(Guid Id) : IRequest;
    public record DeactivateLocalizationResourceCommand(Guid Id) : IRequest;
    public record DeleteLocalizationResourceCommand(Guid Id) : IRequest;
}
