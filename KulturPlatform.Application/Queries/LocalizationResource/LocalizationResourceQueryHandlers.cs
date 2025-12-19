using KulturPlatform.Application.Dtos.LocalizationDto;
using KulturPlatform.Application.Interfaces.LocalizationResource;
using MediatR;

namespace KulturPlatform.Application.Queries.LocalizationResource
{
    public class GetAllLocalizationResourcesQueryHandler : IRequestHandler<GetAllLocalizationResourcesQuery, List<LocalizationResourceDto>>
    {
        private readonly ILocalizationResourceReadService _readService;

        public GetAllLocalizationResourcesQueryHandler(ILocalizationResourceReadService readService)
        {
            _readService = readService;
        }

        public async Task<List<LocalizationResourceDto>> Handle(GetAllLocalizationResourcesQuery request, CancellationToken cancellationToken)
        {
            return await _readService.GetAllAsync();
        }
    }

    public class GetLocalizationResourceByIdQueryHandler : IRequestHandler<GetLocalizationResourceByIdQuery, LocalizationResourceDto?>
    {
        private readonly ILocalizationResourceReadService _readService;

        public GetLocalizationResourceByIdQueryHandler(ILocalizationResourceReadService readService)
        {
            _readService = readService;
        }

        public async Task<LocalizationResourceDto?> Handle(GetLocalizationResourceByIdQuery request, CancellationToken cancellationToken)
        {
            return await _readService.GetByIdAsync(request.Id);
        }
    }

    public class GetLocalizationResourcesBySectionQueryHandler : IRequestHandler<GetLocalizationResourcesBySectionQuery, List<LocalizationResourceDto>>
    {
        private readonly ILocalizationResourceReadService _readService;

        public GetLocalizationResourcesBySectionQueryHandler(ILocalizationResourceReadService readService)
        {
            _readService = readService;
        }

        public async Task<List<LocalizationResourceDto>> Handle(GetLocalizationResourcesBySectionQuery request, CancellationToken cancellationToken)
        {
            return await _readService.GetBySectionAsync(request.Section);
        }
    }
}
