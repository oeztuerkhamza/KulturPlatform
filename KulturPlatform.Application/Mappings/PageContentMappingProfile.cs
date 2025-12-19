using AutoMapper;
using KulturPlatform.Application.Dtos;
using KulturPlatform.Domain.Commons.Aggregates;

namespace KulturPlatform.Application.Mappings
{
    public class PageContentMappingProfile : Profile
    {
        public PageContentMappingProfile()
        {
            CreateMap<PageContent, PageContentDto>()
                .ForMember(dest => dest.PageName, opt => opt.MapFrom(src => src.PageName.Value))
                .ForMember(dest => dest.SectionKey, opt => opt.MapFrom(src => src.SectionKey.Value))
                .ForMember(dest => dest.ContentTr, opt => opt.MapFrom(src => src.ContentTr.Value))
                .ForMember(dest => dest.ContentDe, opt => opt.MapFrom(src => src.ContentDe.Value));
        }
    }
}
