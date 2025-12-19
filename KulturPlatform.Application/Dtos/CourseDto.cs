namespace KulturPlatform.Application.Dtos
{
    public class CourseDto
    {
        public Guid Id { get; set; }

        public string TitleTr { get; set; } = string.Empty;
        public string TitleDe { get; set; } = string.Empty;

        public string DescriptionTr { get; set; } = string.Empty;
        public string DescriptionDe { get; set; } = string.Empty;

        public string? DetailsTr { get; set; }
        public string? DetailsDe { get; set; }

        public string? ScheduleTr { get; set; }
        public string? ScheduleDe { get; set; }

        public string? Icon { get; set; }
        public string? Instructor { get; set; }

        public DateTime? Date { get; set; }
        public string? CourseLocation { get; set; }
        public string? CourseCategory { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public CourseDto()
        {
        }
    }

}
