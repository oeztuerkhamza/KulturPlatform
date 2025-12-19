namespace KulturPlatform.Application.Dtos.TeaEventDto
{
    public sealed record TeaEventAdminDto
    (
        Guid Id,
        string TitleTr,
        string TitleDe,
        bool IsActive,
        DateTime UpdatedAt
    );

}
