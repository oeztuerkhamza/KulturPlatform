namespace KulturPlatform.Application.Commands.ValueItem
{
    public record SectionCommand(
        string HeadingTr,
        string HeadingDe,
        string BodyTr,
        string BodyDe,
        List<SectionItemCommand> Items
    );
}
