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
                activity.ImageUrl?.Value,
                activity.VideoUrl?.Value,

                // Gallery
                activity.GalleryImages.Images
                    .Select(x => x.Value)
                    .ToList()
            );
        }
    }

}
