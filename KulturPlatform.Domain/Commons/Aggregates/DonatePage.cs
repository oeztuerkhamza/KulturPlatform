using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Aggregates
{
    public class DonatePage : AuditableEntity, IAggregateRoot
    {
        // Hero Section
        public Title HeroTitleTurkish { get; private set; }
        public Title HeroTitleGerman { get; private set; }
        public Title HeroSubtitleTurkish { get; private set; }
        public Title HeroSubtitleGerman { get; private set; }
        public Url? HeroImageUrl { get; private set; }

        // Feature Highlights (3 features)
        public Title Feature1TitleTurkish { get; private set; }
        public Title Feature1TitleGerman { get; private set; }
        public Title Feature2TitleTurkish { get; private set; }
        public Title Feature2TitleGerman { get; private set; }
        public Title Feature3TitleTurkish { get; private set; }
        public Title Feature3TitleGerman { get; private set; }

        // Why Donate Section
        public Title WhyDonateTitleTurkish { get; private set; }
        public Title WhyDonateTitleGerman { get; private set; }
        public Description WhyDonateDescriptionTurkish { get; private set; }
        public Description WhyDonateDescriptionGerman { get; private set; }

        // Where Section
        public Title WhereTitleTurkish { get; private set; }
        public Title WhereTitleGerman { get; private set; }
        public Description WhereDescriptionTurkish { get; private set; }
        public Description WhereDescriptionGerman { get; private set; }
        public Description TaxInfoTurkish { get; private set; }
        public Description TaxInfoGerman { get; private set; }

        // Bank Account Details
        public string AccountHolder { get; private set; }
        public string Iban { get; private set; }
        public string BicSwift { get; private set; }
        public string BankName { get; private set; }

        // PayPal Details
        public Url PayPalUrl { get; private set; }
        public string PayPalHandle { get; private set; }

        // Legacy Content (can be deprecated later if not needed)
        public string ContentTurkish { get; private set; }
        public string ContentGerman { get; private set; }

        private DonatePage(Guid id) : base(id) { }

        private DonatePage(
            Guid id,
            Title heroTitleTurkish,
            Title heroTitleGerman,
            Title heroSubtitleTurkish,
            Title heroSubtitleGerman,
            Url? heroImageUrl,
            Title feature1TitleTurkish,
            Title feature1TitleGerman,
            Title feature2TitleTurkish,
            Title feature2TitleGerman,
            Title feature3TitleTurkish,
            Title feature3TitleGerman,
            Title whyDonateTitleTurkish,
            Title whyDonateTitleGerman,
            Description whyDonateDescriptionTurkish,
            Description whyDonateDescriptionGerman,
            Title whereTitleTurkish,
            Title whereTitleGerman,
            Description whereDescriptionTurkish,
            Description whereDescriptionGerman,
            Description taxInfoTurkish,
            Description taxInfoGerman,
            string accountHolder,
            string iban,
            string bicSwift,
            string bankName,
            Url payPalUrl,
            string payPalHandle,
            string contentTurkish,
            string contentGerman)
            : base(id)
        {
            HeroTitleTurkish = heroTitleTurkish;
            HeroTitleGerman = heroTitleGerman;
            HeroSubtitleTurkish = heroSubtitleTurkish;
            HeroSubtitleGerman = heroSubtitleGerman;
            HeroImageUrl = heroImageUrl;

            Feature1TitleTurkish = feature1TitleTurkish;
            Feature1TitleGerman = feature1TitleGerman;
            Feature2TitleTurkish = feature2TitleTurkish;
            Feature2TitleGerman = feature2TitleGerman;
            Feature3TitleTurkish = feature3TitleTurkish;
            Feature3TitleGerman = feature3TitleGerman;

            WhyDonateTitleTurkish = whyDonateTitleTurkish;
            WhyDonateTitleGerman = whyDonateTitleGerman;
            WhyDonateDescriptionTurkish = whyDonateDescriptionTurkish;
            WhyDonateDescriptionGerman = whyDonateDescriptionGerman;

            WhereTitleTurkish = whereTitleTurkish;
            WhereTitleGerman = whereTitleGerman;
            WhereDescriptionTurkish = whereDescriptionTurkish;
            WhereDescriptionGerman = whereDescriptionGerman;
            TaxInfoTurkish = taxInfoTurkish;
            TaxInfoGerman = taxInfoGerman;

            AccountHolder = accountHolder;
            Iban = iban;
            BicSwift = bicSwift;
            BankName = bankName;

            PayPalUrl = payPalUrl;
            PayPalHandle = payPalHandle;

            ContentTurkish = contentTurkish;
            ContentGerman = contentGerman;

            CreatedAt = DateTime.UtcNow;
        }

        public static DonatePage CreateNew(
            Title heroTitleTurkish,
            Title heroTitleGerman,
            Title heroSubtitleTurkish,
            Title heroSubtitleGerman,
            Url? heroImageUrl,
            Title feature1TitleTurkish,
            Title feature1TitleGerman,
            Title feature2TitleTurkish,
            Title feature2TitleGerman,
            Title feature3TitleTurkish,
            Title feature3TitleGerman,
            Title whyDonateTitleTurkish,
            Title whyDonateTitleGerman,
            Description whyDonateDescriptionTurkish,
            Description whyDonateDescriptionGerman,
            Title whereTitleTurkish,
            Title whereTitleGerman,
            Description whereDescriptionTurkish,
            Description whereDescriptionGerman,
            Description taxInfoTurkish,
            Description taxInfoGerman,
            string accountHolder,
            string iban,
            string bicSwift,
            string bankName,
            Url payPalUrl,
            string payPalHandle,
            string contentTurkish,
            string contentGerman)
        {
            return new DonatePage(
                Guid.NewGuid(),
                heroTitleTurkish,
                heroTitleGerman,
                heroSubtitleTurkish,
                heroSubtitleGerman,
                heroImageUrl,
                feature1TitleTurkish,
                feature1TitleGerman,
                feature2TitleTurkish,
                feature2TitleGerman,
                feature3TitleTurkish,
                feature3TitleGerman,
                whyDonateTitleTurkish,
                whyDonateTitleGerman,
                whyDonateDescriptionTurkish,
                whyDonateDescriptionGerman,
                whereTitleTurkish,
                whereTitleGerman,
                whereDescriptionTurkish,
                whereDescriptionGerman,
                taxInfoTurkish,
                taxInfoGerman,
                accountHolder,
                iban,
                bicSwift,
                bankName,
                payPalUrl,
                payPalHandle,
                contentTurkish,
                contentGerman);
        }

        public void Update(
            Title heroTitleTurkish,
            Title heroTitleGerman,
            Title heroSubtitleTurkish,
            Title heroSubtitleGerman,
            Url? heroImageUrl,
            Title feature1TitleTurkish,
            Title feature1TitleGerman,
            Title feature2TitleTurkish,
            Title feature2TitleGerman,
            Title feature3TitleTurkish,
            Title feature3TitleGerman,
            Title whyDonateTitleTurkish,
            Title whyDonateTitleGerman,
            Description whyDonateDescriptionTurkish,
            Description whyDonateDescriptionGerman,
            Title whereTitleTurkish,
            Title whereTitleGerman,
            Description whereDescriptionTurkish,
            Description whereDescriptionGerman,
            Description taxInfoTurkish,
            Description taxInfoGerman,
            string accountHolder,
            string iban,
            string bicSwift,
            string bankName,
            Url payPalUrl,
            string payPalHandle,
            string contentTurkish,
            string contentGerman)
        {
            UpdateHeroSection(heroTitleTurkish, heroTitleGerman, heroSubtitleTurkish, heroSubtitleGerman, heroImageUrl);
            UpdateFeatures(feature1TitleTurkish, feature1TitleGerman, feature2TitleTurkish, feature2TitleGerman, feature3TitleTurkish, feature3TitleGerman);
            UpdateWhySection(whyDonateTitleTurkish, whyDonateTitleGerman, whyDonateDescriptionTurkish, whyDonateDescriptionGerman);
            UpdateWhereSection(whereTitleTurkish, whereTitleGerman, whereDescriptionTurkish, whereDescriptionGerman, taxInfoTurkish, taxInfoGerman);
            UpdateBankDetails(accountHolder, iban, bicSwift, bankName);
            UpdatePayPalDetails(payPalUrl, payPalHandle);
            
            ContentTurkish = contentTurkish;
            ContentGerman = contentGerman;
            
            SetUpdatedAt();
        }

        private void UpdateHeroSection(Title titleTr, Title titleDe, Title subtitleTr, Title subtitleDe, Url? imageUrl)
        {
            HeroTitleTurkish = titleTr;
            HeroTitleGerman = titleDe;
            HeroSubtitleTurkish = subtitleTr;
            HeroSubtitleGerman = subtitleDe;
            HeroImageUrl = imageUrl;
        }

        private void UpdateFeatures(Title f1Tr, Title f1De, Title f2Tr, Title f2De, Title f3Tr, Title f3De)
        {
            Feature1TitleTurkish = f1Tr;
            Feature1TitleGerman = f1De;
            Feature2TitleTurkish = f2Tr;
            Feature2TitleGerman = f2De;
            Feature3TitleTurkish = f3Tr;
            Feature3TitleGerman = f3De;
        }

        private void UpdateWhySection(Title titleTr, Title titleDe, Description descTr, Description descDe)
        {
            WhyDonateTitleTurkish = titleTr;
            WhyDonateTitleGerman = titleDe;
            WhyDonateDescriptionTurkish = descTr;
            WhyDonateDescriptionGerman = descDe;
        }

        private void UpdateWhereSection(Title titleTr, Title titleDe, Description descTr, Description descDe, Description taxTr, Description taxDe)
        {
            WhereTitleTurkish = titleTr;
            WhereTitleGerman = titleDe;
            WhereDescriptionTurkish = descTr;
            WhereDescriptionGerman = descDe;
            TaxInfoTurkish = taxTr;
            TaxInfoGerman = taxDe;
        }

        private void UpdateBankDetails(string accountHolder, string iban, string bicSwift, string bankName)
        {
            AccountHolder = accountHolder;
            Iban = iban;
            BicSwift = bicSwift;
            BankName = bankName;
        }

        private void UpdatePayPalDetails(Url payPalUrl, string payPalHandle)
        {
            PayPalUrl = payPalUrl;
            PayPalHandle = payPalHandle;
        }
    }
}
