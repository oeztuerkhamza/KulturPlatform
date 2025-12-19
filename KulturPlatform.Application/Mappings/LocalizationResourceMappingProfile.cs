using AutoMapper;
using KulturPlatform.Application.Dtos.LocalizationDto;
using KulturPlatform.Domain.Commons.Aggregates;

namespace KulturPlatform.Application.Mappings
{
    public class LocalizationResourceMappingProfile : Profile
    {
        public LocalizationResourceMappingProfile()
        {
            CreateMap<LocalizationResource, LocalizationResourceDto>();
        }
    }
}
