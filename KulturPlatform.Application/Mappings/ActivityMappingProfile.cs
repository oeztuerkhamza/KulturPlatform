using AutoMapper;
using KulturPlatform.Application.Dtos.ActivityDto;
using KulturPlatform.Domain.Commons.AggregateRoot;

namespace KulturPlatform.Application.Mappings
{
    public class ActivityMappingProfile : Profile
    {
        public ActivityMappingProfile()
        {
            // Domain entity → DTO mapping
            CreateMap<Activity, ActivityDto>()
                .ForMember(dest => dest.TitleTr, opt => opt.MapFrom(src => src.TitleTr.Value))
                .ForMember(dest => dest.TitleDe, opt => opt.MapFrom(src => src.TitleDe.Value))
                .ForMember(dest => dest.DescriptionTr, opt => opt.MapFrom(src => src.DescriptionTr.Value))
                .ForMember(dest => dest.DescriptionDe, opt => opt.MapFrom(src => src.DescriptionDe.Value))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src =>
                    $"{src.Address.Street}, {src.Address.City}, {src.Address.State}, {src.Address.Country}, {src.Address.ZipCode}"))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Value))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl != null ? src.ImageUrl.Value : null))
                .ForMember(dest => dest.VideoUrl, opt => opt.MapFrom(src => src.VideoUrl != null ? src.VideoUrl.Value : null))
                .ForMember(dest => dest.GalleryImages, opt => opt.MapFrom(src => src.GalleryImages.Images));
        }
    }
}
