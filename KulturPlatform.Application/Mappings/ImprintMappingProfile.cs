using AutoMapper;
using KulturPlatform.Application.Dtos.ImprintDto;
using KulturPlatform.Application.Dtos.LocalizationDto;
using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.ValueObjects;

namespace KulturPlatform.Application.Mappings
{
    public class ImprintMappingProfile : Profile
    {
        public ImprintMappingProfile()
        {
            CreateMap<Imprint, ImprintDto>()
                .ForMember(dest => dest.OrganizationName, opt => opt.MapFrom(src => src.OrganizationName != null ? src.OrganizationName.Value : null))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email != null ? src.Email.Value : null))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone != null ? src.Phone.Value : null))
                .ForMember(dest => dest.President, opt => opt.MapFrom(src => src.President != null ? src.President.Value : null))
                .ForMember(dest => dest.VicePresident, opt => opt.MapFrom(src => src.VicePresident != null ? src.VicePresident.Value : null))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

            // Address to AddressDto mapping
            CreateMap<Address, AddressDto>()
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.HouseNo, opt => opt.MapFrom(src => src.HouseNo))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country));
        }
    }
}