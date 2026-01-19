using AutoMapper;
using KulturPlatform.Application.Dtos.ContactMessages;

namespace KulturPlatform.Application.Mappings
{
    public class ContactMessageMappingProfile : Profile
    {
        public ContactMessageMappingProfile()
        {
            CreateMap<Domain.Commons.Aggregates.ContactMessage, ContactMessageDto>()
                .ForMember(dest => dest.Anrede, opt => opt.MapFrom(src => src.Anrede != null ? src.Anrede.Value : null))
                .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.SenderName.Value))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone != null ? src.Phone.Value : null))
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject.Value))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message.Value));
        }
    }
}
