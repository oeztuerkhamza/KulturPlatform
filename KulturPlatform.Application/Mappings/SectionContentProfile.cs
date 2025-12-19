using AutoMapper;
using KulturPlatform.Application.Dtos.NewFolder;
using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.ValueObjects;

namespace KulturPlatform.Application.Mappings
{
    public class SectionMappingProfile : Profile
    {
        public SectionMappingProfile()
        {
            // Section → SectionDto mapping
            CreateMap<Section, SectionDto>()
                .ForMember(dest => dest.HeadingTr, opt => opt.MapFrom(src => src.HeadingTr.Value))
                .ForMember(dest => dest.HeadingDe, opt => opt.MapFrom(src => src.HeadingDe.Value))
                .ForMember(dest => dest.BodyTr, opt => opt.MapFrom(src => src.BodyTr.Value))
                .ForMember(dest => dest.BodyDe, opt => opt.MapFrom(src => src.BodyDe.Value));

            // SectionItem → SectionItemDto mapping
            CreateMap<SectionItem, SectionItemDto>()
                .ForMember(dest => dest.TitleTr, opt => opt.MapFrom(src => src.TitleTr.Value))
                .ForMember(dest => dest.TitleDe, opt => opt.MapFrom(src => src.TitleDe.Value))
                .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Icon));
        }
    }
}