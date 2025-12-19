using AutoMapper;
using KulturPlatform.Application.Dtos.AdminDto;
using KulturPlatform.Domain.Commons.Aggregates;

namespace KulturPlatform.Application.Mappings
{
    public class AdminMappingProfile : Profile
    {
        public AdminMappingProfile()
        {
            CreateMap<Admin, AdminDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name != null ? src.Name.Value : null));
        }
    }
}
