using AutoMapper;
using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Dtos.LocalizationDto;
using KulturPlatform.Domain.Commons.Aggregates;

namespace KulturPlatform.Application.Mappings
{
    public class CourseMappingProfile : Profile
    {
        public CourseMappingProfile()
        {
            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.TitleTr, opt => opt.MapFrom(src => src.TitleTr.Value))
                .ForMember(dest => dest.TitleDe, opt => opt.MapFrom(src => src.TitleDe.Value))
                .ForMember(dest => dest.DescriptionTr, opt => opt.MapFrom(src => src.DescriptionTr.Value))
                .ForMember(dest => dest.DescriptionDe, opt => opt.MapFrom(src => src.DescriptionDe.Value))
                .ForMember(dest => dest.DetailsTr, opt => opt.MapFrom(src => src.DetailsTr != null ? src.DetailsTr.Value : null))
                .ForMember(dest => dest.DetailsDe, opt => opt.MapFrom(src => src.DetailsDe != null ? src.DetailsDe.Value : null))
                .ForMember(dest => dest.ScheduleTr, opt => opt.MapFrom(src => src.ScheduleTr != null ? src.ScheduleTr.Value : null))
                .ForMember(dest => dest.ScheduleDe, opt => opt.MapFrom(src => src.ScheduleDe != null ? src.ScheduleDe.Value : null))
                .ForMember(dest => dest.Instructor, opt => opt.MapFrom(src => src.Instructor != null ? src.Instructor.Value : null))
                .ForMember(dest => dest.CourseLocation, opt => opt.MapFrom(src =>
                    src.CourseLocation != null ? new AddressDto
                    {
                        Street = src.CourseLocation.Street,
                        HouseNo = src.CourseLocation.HouseNo,
                        ZipCode = src.CourseLocation.ZipCode,
                        City = src.CourseLocation.City,
                        State = src.CourseLocation.State,
                        Country = src.CourseLocation.Country
                    } : null))
                .ForMember(dest => dest.CourseCategory, opt => opt.MapFrom(src => src.CourseCategory != null ? src.CourseCategory.Value : null));
        }
    }
}
