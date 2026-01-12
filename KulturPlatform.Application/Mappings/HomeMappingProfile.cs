using AutoMapper;
using KulturPlatform.Application.Dtos.Home;
using KulturPlatform.Domain.Commons.AggregateRoot;

namespace KulturPlatform.Application.Mappings
{
    public class HomeMappingProfile : Profile
    {
        public HomeMappingProfile()
        {
            CreateMap<HeroSection, HeroSectionDto>()
                .ForMember(dest => dest.TitleTr, opt => opt.MapFrom(src => src.TitleTr.Value))
                .ForMember(dest => dest.TitleDe, opt => opt.MapFrom(src => src.TitleDe.Value))
                .ForMember(dest => dest.SubtitleTr, opt => opt.MapFrom(src => src.SubtitleTr.Value))
                .ForMember(dest => dest.SubtitleDe, opt => opt.MapFrom(src => src.SubtitleDe.Value))
                .ForMember(dest => dest.DescriptionTr, opt => opt.MapFrom(src => src.DescriptionTr.Value))
                .ForMember(dest => dest.DescriptionDe, opt => opt.MapFrom(src => src.DescriptionDe.Value))
                .ForMember(dest => dest.BackgroundImageUrl, opt => opt.MapFrom(src => src.BackgroundImageUrl.Value))
                .ForMember(dest => dest.PrimaryButtonTextTr, opt => opt.MapFrom(src => src.PrimaryButtonTextTr.Value))
                .ForMember(dest => dest.PrimaryButtonTextDe, opt => opt.MapFrom(src => src.PrimaryButtonTextDe.Value))
                .ForMember(dest => dest.SecondaryButtonTextTr, opt => opt.MapFrom(src => src.SecondaryButtonTextTr.Value))
                .ForMember(dest => dest.SecondaryButtonTextDe, opt => opt.MapFrom(src => src.SecondaryButtonTextDe.Value));

            CreateMap<CtaSection, CtaSectionDto>()
                .ForMember(dest => dest.TitleTr, opt => opt.MapFrom(src => src.TitleTr.Value))
                .ForMember(dest => dest.TitleDe, opt => opt.MapFrom(src => src.TitleDe.Value))
                .ForMember(dest => dest.DescriptionTr, opt => opt.MapFrom(src => src.DescriptionTr.Value))
                .ForMember(dest => dest.DescriptionDe, opt => opt.MapFrom(src => src.DescriptionDe.Value))
                .ForMember(dest => dest.PrimaryButtonTr, opt => opt.MapFrom(src => src.PrimaryButtonTr.Value))
                .ForMember(dest => dest.PrimaryButtonDe, opt => opt.MapFrom(src => src.PrimaryButtonDe.Value))
                .ForMember(dest => dest.SecondaryButtonTr, opt => opt.MapFrom(src => src.SecondaryButtonTr.Value))
                .ForMember(dest => dest.SecondaryButtonDe, opt => opt.MapFrom(src => src.SecondaryButtonDe.Value))
                .ForMember(dest => dest.DonateButtonTr, opt => opt.MapFrom(src => src.DonateButtonTr.Value))
                .ForMember(dest => dest.DonateButtonDe, opt => opt.MapFrom(src => src.DonateButtonDe.Value));

            CreateMap<Feature, FeatureDto>()
                .ForMember(dest => dest.TitleTr, opt => opt.MapFrom(src => src.TitleTr.Value))
                .ForMember(dest => dest.TitleDe, opt => opt.MapFrom(src => src.TitleDe.Value))
                .ForMember(dest => dest.DescriptionTr, opt => opt.MapFrom(src => src.DescriptionTr.Value))
                .ForMember(dest => dest.DescriptionDe, opt => opt.MapFrom(src => src.DescriptionDe.Value))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color));

            CreateMap<InstagramPost, InstagramPostDto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl.Value))
                .ForMember(dest => dest.Link, opt => opt.MapFrom(src => src.Link != null ? src.Link.Value : null));
        }
    }
}
