namespace KulturPlatform.Application.Dtos.NewFolder
{
    public record SectionDto(
        string HeadingTr,
        string HeadingDe,
        string BodyTr,
        string BodyDe,
        List<SectionItemDto> Items
    );
}
