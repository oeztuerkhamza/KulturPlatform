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
                .ForMember(dest => dest.GalleryImages, opt => opt.MapFrom(src => ConvertDomainGalleryToDto(src.GalleryImages.Images)));

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
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => !string.IsNullOrWhiteSpace(src.ImageUrl) ? Image.Create(src.ImageUrl) : null))
                .ForMember(dest => dest.VideoUrl, opt => opt.MapFrom(src => !string.IsNullOrWhiteSpace(src.VideoUrl) ? Image.Create(src.VideoUrl) : null))
                .ForMember(dest => dest.GalleryImages, opt => opt.MapFrom(src => ConvertDtoGalleryToDomain(src.GalleryImages)));
        }

        /// <summary>
        /// Converts domain GalleryImage objects to DTO GalleryImageDto objects
        /// </summary>
        private static List<GalleryImageDto> ConvertDomainGalleryToDto(IEnumerable<GalleryImage> domainImages)
        {
            return domainImages.Select(img => ConvertGalleryImageToDto(img.GetImageSource())).ToList();
        }

        /// <summary>
        /// Converts a single image source string to GalleryImageDto
        /// </summary>
        private static GalleryImageDto ConvertGalleryImageToDto(string rawImage)
        {
            if (string.IsNullOrEmpty(rawImage))
                return new GalleryImageDto { Url = null, Base64Data = null, FileName = null };

            // If it's a URL (doesn't start with data:)
            if (!rawImage.StartsWith("data:"))
                return new GalleryImageDto { Url = rawImage, Base64Data = null, FileName = null };

            // If it's a data URI: data:image/jpeg;base64,xxxxx
            var parts = rawImage.Split(",", 2);
            if (parts.Length == 2)
            {
                return new GalleryImageDto
                {
                    Url = null,
                    Base64Data = parts[1], // Extract base64 part only
                    FileName = null
                };
            }

            return new GalleryImageDto { Url = null, Base64Data = null, FileName = null };
        }

        /// <summary>
        /// Converts DTO GalleryImageDto objects back to domain MediaGallery
        /// </summary>
        private static MediaGallery ConvertDtoGalleryToDomain(List<GalleryImageDto> dtoImages)
        {
            if (dtoImages == null || !dtoImages.Any())
                return new MediaGallery(new List<string>());

            var rawImages = dtoImages.Select(dto =>
            {
                // Convert DTO back to raw image string
                if (!string.IsNullOrEmpty(dto.Url))
                    return dto.Url;
                if (!string.IsNullOrEmpty(dto.Base64Data))
                    return $"data:image/jpeg;base64,{dto.Base64Data}";
                return string.Empty;
            }).Where(s => !string.IsNullOrEmpty(s)).ToList();

            return new MediaGallery(rawImages);
        }
    }
}
