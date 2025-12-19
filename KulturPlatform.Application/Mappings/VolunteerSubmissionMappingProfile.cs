using AutoMapper;
using KulturPlatform.Application.Dtos;
using KulturPlatform.Domain.Commons.Aggregates;

namespace KulturPlatform.Application.Mappings
{
    public class VolunteerSubmissionMappingProfile : Profile
    {
        public VolunteerSubmissionMappingProfile()
        {
            CreateMap<VolunteerSubmission, VolunteerSubmissionDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName.Value))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber.Value))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message.Value));
        }
    }
}
