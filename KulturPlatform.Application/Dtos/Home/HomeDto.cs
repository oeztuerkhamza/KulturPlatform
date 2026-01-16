using KulturPlatform.Application.Dtos.Activity;

namespace KulturPlatform.Application.Dtos.Home
{
    public record HomeDto(
        HeroSectionDto? HeroSection,
        List<FeatureDto> Features,
        CtaSectionDto? Cta,
        List<InstagramPostDto> InstagramFeed
    );
}
