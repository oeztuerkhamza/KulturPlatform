namespace KulturPlatform.Application.Dtos.TeaEventDto
{
    public record class TeaEventDto
    {
        public Guid Id { get; init; }
        public string TitleTr { get; init; }
        public string TitleDe { get; init; }
        public string IntroTr { get; init; }
        public string IntroDe { get; init; }
        public string HeritageTextTr { get; init; }
        public string HeritageTextDe { get; init; }
        public string ParticipationTextTr { get; init; }
        public string ParticipationTextDe { get; init; }
        public string Date { get; init; }
        public string Time { get; init; }
        public string Location { get; init; }
        public string ImageUrl { get; init; }
        public string ContactEmail { get; init; }
        public bool IsActive { get; init; }
    }

}
