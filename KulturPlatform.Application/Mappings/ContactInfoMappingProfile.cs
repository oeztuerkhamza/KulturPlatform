using AutoMapper;
using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Dtos.LocalizationDto;
using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.ValueObjects;

namespace KulturPlatform.Application.Mappings
{
    public class ContactInfoMappingProfile : Profile
    {
        public ContactInfoMappingProfile()
        {
            CreateMap<ContactInfo, ContactInfoDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone.Value))
                .ForMember(dest => dest.OfficeHours, opt => opt.MapFrom(src => src.OfficeHours))
                .ForMember(dest => dest.SocialMedia, opt => opt.MapFrom(src => src.SocialMedia))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

            CreateMap<SocialMediaLinks, SocialMediaLinksDto>();
            CreateMap<Address, AddressDto>()
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.HouseNo, opt => opt.MapFrom(src => src.HouseNo))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country));

        }
    }
}
