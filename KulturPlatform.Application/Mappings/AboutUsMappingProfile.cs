using AutoMapper;
using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Dtos.AboutUs;
using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;

namespace KulturPlatform.Application.Mappings
{
    public class AboutUsMappingProfile : Profile
    {
        public AboutUsMappingProfile()
        {
            CreateMap<Name, NameDto>();
            CreateMap<Title, TitleDto>();
            CreateMap<Description, DescriptionDto>();

            CreateMap<AboutUsItem, AboutUsItemDto>();
            CreateMap<TeamMember, TeamMemberDto>();
            CreateMap<AboutUs, AboutUsDto>();
        }
    }

}
