namespace KulturPlatform.Application.Dtos
{
    public class GuelenMovementDto
    {
        public Guid Id { get; set; }
        public string TitleTr { get; set; }
        public string TitleDe { get; set; }
        public string ContentTr { get; set; }
        public string ContentDe { get; set; }
        public string ImageUrl { get; set; }

        public GuelenMovementDto() { }
    }
}
