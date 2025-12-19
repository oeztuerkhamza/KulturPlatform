namespace KulturPlatform.Application.Dtos
{
    public class DonatePageDto
    {
        public Guid Id { get; set; }
        public string HeroTitleTr { get; set; }
        public string HeroTitleDe { get; set; }
        public string HeroSubtitleTr { get; set; }
        public string HeroSubtitleDe { get; set; }
        public string HeroImageUrl { get; set; }
        public string AccountHolder { get; set; }
        public string Iban { get; set; }
        public string BankName { get; set; }
        public string ContentTr { get; set; }
        public string ContentDe { get; set; }

        public DonatePageDto() { }
    }
}
