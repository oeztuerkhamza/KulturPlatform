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
        
        /// <summary>
        /// Image source - either URL or data URI (base64) for display
        /// </summary>
        public string? ImageSource { get; init; }
        
        public string ContactEmail { get; init; }
        public bool IsActive { get; init; }
    }

}
