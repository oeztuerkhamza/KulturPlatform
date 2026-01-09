using AutoMapper;
using KulturPlatform.Application.Dtos.Activity;
using KulturPlatform.Application.Dtos.LocalizationDto;
using KulturPlatform.Domain.Commons.ValueObjects;

namespace KulturPlatform.Application.Mappings
{
    public class ActivityMapperProfile : Profile
    {
        public ActivityMapperProfile()
        {
            // Domain -> DTO
            CreateMap<Domain.Commons.AggregateRoot.Activity, ActivityDto>()
                .ForMember(dest => dest.TitleTr, opt => opt.MapFrom(src => src.TitleTr.Value))
                .ForMember(dest => dest.TitleDe, opt => opt.MapFrom(src => src.TitleDe.Value))
                .ForMember(dest => dest.DescriptionTr, opt => opt.MapFrom(src => src.DescriptionTr.Value))
                .ForMember(dest => dest.DescriptionDe, opt => opt.MapFrom(src => src.DescriptionDe.Value))
                .ForMember(dest => dest.DetailedContentTr, opt => opt.MapFrom(src => src.DetailedContentTr != null ? src.DetailedContentTr.Value : null))
                .ForMember(dest => dest.DetailedContentDe, opt => opt.MapFrom(src => src.DetailedContentDe != null ? src.DetailedContentDe.Value : null))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.DateIso)) // DateTime
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new AddressDto
                {
                    Street = src.Address.Street,
                    HouseNo = src.Address.HouseNo,
                    ZipCode = src.Address.ZipCode,
                    City = src.Address.City,
                    State = src.Address.State,
                    Country = src.Address.Country
                }))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Value))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl != null ? src.ImageUrl.Value : null))
                .ForMember(dest => dest.VideoUrl, opt => opt.MapFrom(src => src.VideoUrl != null ? src.VideoUrl.Value : null))
                .ForMember(dest => dest.GalleryImages, opt => opt.MapFrom(src => src.GalleryImages.Images));

            // DTO -> Domain (örn. Create / Update Command maplerinde)
            CreateMap<ActivityDto, Domain.Commons.AggregateRoot.Activity>()
                .ForMember(dest => dest.TitleTr, opt => opt.MapFrom(src => new Title(src.TitleTr)))
                .ForMember(dest => dest.TitleDe, opt => opt.MapFrom(src => new Title(src.TitleDe)))
                .ForMember(dest => dest.DescriptionTr, opt => opt.MapFrom(src => new Description(src.DescriptionTr)))
                .ForMember(dest => dest.DescriptionDe, opt => opt.MapFrom(src => new Description(src.DescriptionDe)))
                .ForMember(dest => dest.DetailedContentTr, opt => opt.MapFrom(src => src.DetailedContentTr != null ? new LocalizedContent(src.DetailedContentTr) : null))
                .ForMember(dest => dest.DetailedContentDe, opt => opt.MapFrom(src => src.DetailedContentDe != null ? new LocalizedContent(src.DetailedContentDe) : null))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => ActivityDate.FromString(src.Date)))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address(
                    src.Address.Street,
                    src.Address.HouseNo,
                    src.Address.ZipCode,
                    src.Address.City,
                    src.Address.State,
                    src.Address.Country)))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new Category(src.Category)))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => !string.IsNullOrWhiteSpace(src.ImageUrl) ? Url.Create(src.ImageUrl) : null))
                .ForMember(dest => dest.VideoUrl, opt => opt.MapFrom(src => !string.IsNullOrWhiteSpace(src.VideoUrl) ? Url.Create(src.VideoUrl) : null))
                .ForMember(dest => dest.GalleryImages, opt => opt.MapFrom(src => src.GalleryImages != null ? new MediaGallery(src.GalleryImages) : new MediaGallery(new List<string>())));
        }
    }
}
