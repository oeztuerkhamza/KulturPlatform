namespace KulturPlatform.Application.Dtos.NewFolder
{
    public record ValueItemListDto(
        Guid Id,
        string TitleTr,
        string TitleDe,
        int DisplayOrder,
        bool IsActive
    );
}
