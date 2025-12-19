using AutoMapper;
using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Dtos.NewFolder;
using KulturPlatform.Domain.Commons.Aggregates;

namespace KulturPlatform.Application.Mappings
{
    public class ValueItemMappingProfile : Profile
    {
        public ValueItemMappingProfile()
        {
            CreateMap<ValueItem, ValueItemDto>()
                .ForMember(dest => dest.TitleTr, opt => opt.MapFrom(src => src.TitleTr.Value))
                .ForMember(dest => dest.TitleDe, opt => opt.MapFrom(src => src.TitleDe.Value))
                .ForMember(dest => dest.SubtitleTr, opt => opt.MapFrom(src => src.SubtitleTr != null ? src.SubtitleTr.Value : null))
                .ForMember(dest => dest.SubtitleDe, opt => opt.MapFrom(src => src.SubtitleDe != null ? src.SubtitleDe.Value : null))
                .ForMember(dest => dest.DescriptionTr, opt => opt.MapFrom(src => src.DescriptionTr.Value))
                .ForMember(dest => dest.DescriptionDe, opt => opt.MapFrom(src => src.DescriptionDe.Value))
                .ForMember(dest => dest.DisplayOrder, opt => opt.MapFrom(src => src.DisplayOrder.Value))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));


            CreateMap<ValueItem, ValueItemDetailDto>()
                .ForMember(dest => dest.TitleTr, opt => opt.MapFrom(src => src.TitleTr.Value))
                .ForMember(dest => dest.TitleDe, opt => opt.MapFrom(src => src.TitleDe.Value))
                .ForMember(dest => dest.SubtitleTr, opt => opt.MapFrom(src => src.SubtitleTr != null ? src.SubtitleTr.Value : null))
                .ForMember(dest => dest.SubtitleDe, opt => opt.MapFrom(src => src.SubtitleDe != null ? src.SubtitleDe.Value : null))
                .ForMember(dest => dest.IntroTr, opt => opt.MapFrom(src => src.DescriptionTr.Value))
                .ForMember(dest => dest.IntroDe, opt => opt.MapFrom(src => src.DescriptionDe.Value))
                .ForMember(dest => dest.CtaButtonTr, opt => opt.MapFrom(src => (string?)null))
                .ForMember(dest => dest.CtaButtonDe, opt => opt.MapFrom(src => (string?)null))
                .ForMember(dest => dest.DisplayOrder, opt => opt.MapFrom(src => src.DisplayOrder.Value))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));


        }
    }
}