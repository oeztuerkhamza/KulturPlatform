namespace KulturPlatform.Application.Dtos
{
    public class DonatePageDto
    {
        public Guid Id { get; set; }
        
        // Hero Section
        public string HeroTitleTr { get; set; }
        public string HeroTitleDe { get; set; }
        public string HeroSubtitleTr { get; set; }
        public string HeroSubtitleDe { get; set; }
        public string HeroImageUrl { get; set; }

        // Feature Highlights
        public string Feature1TitleTr { get; set; }
        public string Feature1TitleDe { get; set; }
        public string Feature2TitleTr { get; set; }
        public string Feature2TitleDe { get; set; }
        public string Feature3TitleTr { get; set; }
        public string Feature3TitleDe { get; set; }

        // Why Donate Section
        public string WhyDonateTitleTr { get; set; }
        public string WhyDonateTitleDe { get; set; }
        public string WhyDonateDescriptionTr { get; set; }
        public string WhyDonateDescriptionDe { get; set; }

        // Where Section
        public string WhereTitleTr { get; set; }
        public string WhereTitleDe { get; set; }
        public string WhereDescriptionTr { get; set; }
        public string WhereDescriptionDe { get; set; }
        public string TaxInfoTr { get; set; }
        public string TaxInfoDe { get; set; }

        // Bank Account Details
        public string AccountHolder { get; set; }
        public string Iban { get; set; }
        public string BicSwift { get; set; }
        public string BankName { get; set; }

        // PayPal Details
        public string PayPalUrl { get; set; }
        public string PayPalHandle { get; set; }

        // Legacy Content
        public string ContentTr { get; set; }
        public string ContentDe { get; set; }

        public DonatePageDto() { }
    }
}
