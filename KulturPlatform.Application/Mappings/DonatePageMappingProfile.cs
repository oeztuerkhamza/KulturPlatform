using AutoMapper;
using KulturPlatform.Application.Commands.DonatePage;
using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.ValueObjects;

namespace KulturPlatform.Application.Mappings
{
    public class DonatePageMappingProfile : Profile
    {
        public DonatePageMappingProfile()
        {
            CreateMap<UpdateDonatePageCommand, DonatePage>()
                .ForMember(dest => dest.HeroTitleTurkish, opt => opt.MapFrom(src => Title.Create(src.HeroTitleTr)))
                .ForMember(dest => dest.HeroTitleGerman, opt => opt.MapFrom(src => Title.Create(src.HeroTitleDe)))
                .ForMember(dest => dest.HeroSubtitleTurkish, opt => opt.MapFrom(src => Title.Create(src.HeroSubtitleTr)))
                .ForMember(dest => dest.HeroSubtitleGerman, opt => opt.MapFrom(src => Title.Create(src.HeroSubtitleDe)))
                .ForMember(dest => dest.HeroImageUrl, opt => opt.MapFrom(src => Url.Create(src.HeroImageUrl)));
        }
    }

}
