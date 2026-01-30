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
                .ConstructUsing(src => new HeroSectionDto(
                    src.Id,
                    src.TitleTr.Value,
                    src.TitleDe.Value,
                    src.SubtitleTr.Value,
                    src.SubtitleDe.Value,
                    src.DescriptionTr.Value,
                    src.DescriptionDe.Value,
                    src.BackgroundImageUrl != null ? src.BackgroundImageUrl.Value : null,
                    src.BackgroundImageData != null ? src.BackgroundImageData.Base64Data : null,
                    src.BackgroundImageData != null ? src.BackgroundImageData.FileName : null,
                    src.PrimaryButtonTextTr.Value,
                    src.PrimaryButtonTextDe.Value,
                    src.SecondaryButtonTextTr.Value,
                    src.SecondaryButtonTextDe.Value
                ));

            CreateMap<CtaSection, CtaSectionDto>()
                .ConstructUsing(src => new CtaSectionDto(
                    src.Id,
                    src.TitleTr.Value,
                    src.TitleDe.Value,
                    src.DescriptionTr.Value,
                    src.DescriptionDe.Value,
                    src.BackgroundImageUrl != null ? src.BackgroundImageUrl.Value : null,
                    src.BackgroundImageData != null ? src.BackgroundImageData.Base64Data : null,
                    src.BackgroundImageData != null ? src.BackgroundImageData.FileName : null,
                    src.PrimaryButtonTr.Value,
                    src.PrimaryButtonDe.Value,
                    src.SecondaryButtonTr.Value,
                    src.SecondaryButtonDe.Value,
                    src.DonateButtonTr.Value,
                    src.DonateButtonDe.Value
                ));

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
