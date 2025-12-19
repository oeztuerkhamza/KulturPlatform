using AutoMapper;
using KulturPlatform.Application.Dtos.AboutUs;
using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.ValueObjects;

namespace KulturPlatform.Application.Mappings
{
    public class AboutUsMappingProfile : Profile
    {
        public AboutUsMappingProfile()
        {
            CreateMap<AboutUs, AboutUsDto>()
                .ForMember(d => d.Key, o => o.MapFrom(s => s.Key))
                .ForMember(d => d.Quote, o => o.MapFrom(s => s.Quote))
                .ForMember(d => d.QuoteAuthor, o => o.MapFrom(s => s.QuoteAuthor))
                .ForMember(d => d.Sections, o => o.MapFrom(s => s.Sections))
                .ForMember(d => d.CreatedAt, o => o.MapFrom(s => s.CreatedAt))
                .ForMember(d => d.UpdatedAt, o => o.MapFrom(s => s.UpdatedAt));

            CreateMap<NamedSection, NamedSectionDto>()
                .ForMember(d => d.Key, o => o.MapFrom(s => s.Key))
                .ForMember(d => d.Content, o => o.MapFrom(s => s.Content));

            CreateMap<SectionContent, SectionContentDto>()
                .ForMember(d => d.Heading, o => o.MapFrom(s => s.Heading))
                .ForMember(d => d.BodyTurkish, o => o.MapFrom(s => s.BodyTurkish))
                .ForMember(d => d.BodyGerman, o => o.MapFrom(s => s.BodyGerman));
        }
    }
}
