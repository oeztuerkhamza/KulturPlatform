namespace KulturPlatform.Application.Dtos.AboutUs
{
    public class AboutUsItemDto
    {
        public TitleDto TitleTr { get; set; }
        public TitleDto TitleDe { get; set; }
        public DescriptionDto DescriptionTr { get; set; }
        public DescriptionDto DescriptionDe { get; set; }
        public int Order { get; set; }
    }
}
