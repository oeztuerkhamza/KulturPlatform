namespace KulturPlatform.Application.Dtos.NewFolder
{
    public record VolunteerContentDto(
        Guid Id,
        string TitleTr,
        string TitleDe,
        string SubtitleTr,
        string SubtitleDe,
        string IntroTr,
        string IntroDe,
        SectionDto NameAndPurpose,
        SectionDto Why,
        SectionDto Who,
        SectionDto How,
        string CtaButtonTr,
        string CtaButtonDe
    );
}
