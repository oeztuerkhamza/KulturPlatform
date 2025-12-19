namespace KulturPlatform.Application.Dtos
{
    public class ActivityDto
    {
        public Guid Id { get; set; }

        public string TitleTr { get; set; }
        public string TitleDe { get; set; }

        public string DescriptionTr { get; set; }
        public string DescriptionDe { get; set; }

        public string? DetailedContentTr { get; set; }
        public string? DetailedContentDe { get; set; }

        public DateTime DateTr { get; set; }
        public DateTime DateDe { get; set; }

        public string Location { get; set; }
        public string Category { get; set; }

        public string? ImageUrl { get; set; }
        public List<string> GalleryImages { get; set; } = new();
        public string? VideoUrl { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ActivityDto()
        {
        }
    }
}
