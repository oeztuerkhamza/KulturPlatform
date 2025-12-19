namespace KulturPlatform.Application.Dtos
{
    public class PageContentDto
    {
        public Guid Id { get; set; }

        public string PageName { get; set; } = string.Empty;
        public string SectionKey { get; set; } = string.Empty;

        public string ContentTr { get; set; } = string.Empty;
        public string ContentDe { get; set; } = string.Empty;

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public PageContentDto()
        {
        }
    }

}
