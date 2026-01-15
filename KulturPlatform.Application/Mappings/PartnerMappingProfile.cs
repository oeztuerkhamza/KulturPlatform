using AutoMapper;
using KulturPlatform.Application.Dtos;
using KulturPlatform.Domain.Commons.Aggregates;

namespace KulturPlatform.Application.Mappings
{
    public class PartnerMappingProfile : Profile
    {
        public PartnerMappingProfile()
        {
            CreateMap<Partner, PartnerDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value))
                .ForMember(dest => dest.DescriptionTr, opt => opt.MapFrom(src => src.DescriptionTr.Value))
                .ForMember(dest => dest.DescriptionDe, opt => opt.MapFrom(src => src.DescriptionDe.Value))
                .ForMember(dest => dest.LogoUrl, opt => opt.MapFrom(src => src.LogoUrl != null ? src.LogoUrl.Value : null))
                .ForMember(dest => dest.WebsiteUrl, opt => opt.MapFrom(src => src.WebsiteUrl != null ? src.WebsiteUrl.Value : null))
                .ForMember(dest => dest.DisplayOrder, opt => opt.MapFrom(src => src.DisplayOrder.Value));
        }
    }
}
