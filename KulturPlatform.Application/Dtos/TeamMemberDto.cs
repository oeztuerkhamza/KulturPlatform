namespace KulturPlatform.Application.Dtos
{
    public class TeamMemberDto
    {
        public NameDto Name { get; set; }
        public TitleDto TitleTr { get; set; }
        public TitleDto TitleDe { get; set; }
        public string ImageUrl { get; set; }
        public int Order { get; set; }
    }


}
