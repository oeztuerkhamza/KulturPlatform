namespace KulturPlatform.Application.Dtos.AboutUs
{
    public class UpdateAboutUsCommandDto
    {
        public DescriptionDto QuoteTr { get; set; }
        public DescriptionDto QuoteDe { get; set; }
        public string QuoteAuthor { get; set; }

        public DescriptionDto WhoWeAreTr { get; set; }
        public DescriptionDto WhoWeAreDe { get; set; }
        public DescriptionDto GoalsTr { get; set; }
        public DescriptionDto GoalsDe { get; set; }
        public DescriptionDto VisionTr { get; set; }
        public DescriptionDto VisionDe { get; set; }
        public DescriptionDto MissionTr { get; set; }
        public DescriptionDto MissionDe { get; set; }

        public List<AboutUsItemDto> CoreValues { get; set; }
        public List<AboutUsItemDto> FocusAreas { get; set; }
        public List<AboutUsItemDto> ActivityAreas { get; set; }

        public List<TeamMemberDto> TeamMembers { get; set; }
    }
}
