using KulturPlatform.Application.Dtos.LocalizationDto;
using MediatR;

namespace KulturPlatform.Application.Queries.LocalizationResource
{
    public record GetAllLocalizationResourcesQuery : IRequest<List<LocalizationResourceDto>>;
    public record GetLocalizationResourceByIdQuery(Guid Id) : IRequest<LocalizationResourceDto?>;
    public record GetLocalizationResourcesBySectionQuery(string Section) : IRequest<List<LocalizationResourceDto>>;
}
