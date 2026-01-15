namespace KulturPlatform.Application.Dtos
{
    public class TeamMemberDto
    {
        public Guid Id { get; set; }
        public NameDto Name { get; set; }
        public TitleDto TitleTr { get; set; }
        public TitleDto TitleDe { get; set; }
        public DescriptionDto? DescriptionTr { get; set; }
        public DescriptionDto? DescriptionDe { get; set; }
        public string ImageUrl { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
