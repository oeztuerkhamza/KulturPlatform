using AutoMapper;
using KulturPlatform.Application.Dtos.Activity;
using KulturPlatform.Application.Dtos.LocalizationDto;
using KulturPlatform.Domain.Commons.AggregateRoot;

namespace KulturPlatform.Application.Mappings
{
    public class ActivityMappingProfile : Profile
    {
        public ActivityMappingProfile()
        {
            CreateMap<Activity, ActivityDto>()
                .ConstructUsing(src => new ActivityDto(
                    src.Id,
                    src.TitleTr.Value,
                    src.TitleDe.Value,
                    src.DescriptionTr.Value,
                    src.DescriptionDe.Value,
                    src.DetailedContentTr != null ? src.DetailedContentTr.Value : null,
                    src.DetailedContentDe != null ? src.DetailedContentDe.Value : null,
                    src.Date.DateIso.ToString("yyyy-MM-dd"),
                    new AddressDto
                    {
                        Street = src.Address.Street,
                        HouseNo = src.Address.HouseNo,
                        ZipCode = src.Address.ZipCode,
                        City = src.Address.City,
                        State = src.Address.State,
                        Country = src.Address.Country
                    },
                    src.Category.Value,
                    src.ImageUrl != null ? src.ImageUrl.Value : null, // Deprecated
                    src.GetImageSource(), // New: Unified image source
                    CreateImageMetadata(src), // New: Image metadata
                    ConvertGalleryImages(src.GalleryImages.GetImageSources()), // ✅ Convert to GalleryImageDto objects
                    src.VideoUrl != null ? src.VideoUrl.Value : null,
                    src.IsActive
                ));
        }

        private static ImageMetadataDto? CreateImageMetadata(Activity activity)
        {
            if (activity.ImageData != null)
            {
                return new ImageMetadataDto(
                    StorageType: "Database",
                    MimeType: activity.ImageData.MimeType,
                    FileName: activity.ImageData.FileName,
                    FileSizeKB: activity.ImageData.FileSizeBytes / 1024
                );
            }
            else if (activity.ImageUrl != null)
            {
                return new ImageMetadataDto(
                    StorageType: "URL",
                    MimeType: null,
                    FileName: null,
                    FileSizeKB: null
                );
            }

            return null;
        }

        /// <summary>
        /// Converts raw image sources to GalleryImageDto objects
        /// </summary>
        private static List<GalleryImageDto> ConvertGalleryImages(IEnumerable<string> rawImages)
        {
            return rawImages.Select(ConvertGalleryImage).ToList();
        }

        /// <summary>
        /// Converts a single raw image string to GalleryImageDto
        /// </summary>
        private static GalleryImageDto ConvertGalleryImage(string rawImage)
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

            // Fallback for malformed data
            return new GalleryImageDto { Url = null, Base64Data = null, FileName = null };
        }
    }
}
