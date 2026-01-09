using KulturPlatform.Application.Dtos.Activity;

namespace KulturPlatform.Application.Dtos.Home
{
    public record HomeDto(
        List<ActivityDto> Activities,
        List<FeatureDto> Features,
        CtaSectionDto Cta,
        List<InstagramPostDto> InstagramFeed
    );
}
