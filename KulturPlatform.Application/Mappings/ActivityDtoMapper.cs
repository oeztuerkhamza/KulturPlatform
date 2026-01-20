using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Dtos.Activity;
using KulturPlatform.Domain.Commons.AggregateRoot;

namespace KulturPlatform.Application.Mappings
{
    public static class ActivityDtoMapper
    {
        public static ActivityDetailDto ToDetailDto(Activity activity)
        {
            return new ActivityDetailDto(
                activity.Id,

                // Title
                new TranslationDto(
                    activity.TitleTr.Value,
                    activity.TitleDe.Value
                ),

                // Description
                new TranslationDto(
                    activity.DescriptionTr.Value,
                    activity.DescriptionDe.Value
                ),

                // Detailed content (nullable)
                activity.DetailedContentTr is null
                    ? null
                    : new TranslationDto(
                        activity.DetailedContentTr.Value,
                        activity.DetailedContentDe.Value
                    ),

                // Date
                new TranslationDto(
                    activity.Date.ToTrString(),
                    activity.Date.ToDeString()
                ),

                // Address
                activity.Address.ToString(),

                // Category (frontend key uyumu)
                activity.Category.ToString().ToLowerInvariant(),

                // Media
                activity.GetImageSource(),
                activity.VideoUrl?.Value,

                // Gallery - convert to GalleryImageDto objects
                ConvertGalleryImages(activity.GalleryImages.GetImageSources())
            );
        }

        /// <summary>
        /// Converts raw image sources (URLs or base64 data URIs) to GalleryImageDto objects
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
