using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Aggregates
{
    public class DonatePage : AuditableEntity, IAggregateRoot
    {
        public Title HeroTitleTurkish { get; private set; }
        public Title HeroTitleGerman { get; private set; }
        public Title HeroSubtitleTurkish { get; private set; }
        public Title HeroSubtitleGerman { get; private set; }
        public Url HeroImageUrl { get; private set; }
        public string AccountHolder { get; private set; }
        public string Iban { get; private set; }
        public string BankName { get; private set; }
        public string ContentTurkish { get; private set; }
        public string ContentGerman { get; private set; }

        private DonatePage(Guid id) : base(id) { }

        private DonatePage(Guid id, Title heroTitleTurkish, Title heroTitleGerman,
            Title heroSubtitleTurkish, Title heroSubtitleGerman, Url heroImageUrl,
            string accountHolder, string iban, string bankName,
            string contentTurkish, string contentGerman)
            : base(id)
        {
            HeroTitleTurkish = heroTitleTurkish;
            HeroTitleGerman = heroTitleGerman;
            HeroSubtitleTurkish = heroSubtitleTurkish;
            HeroSubtitleGerman = heroSubtitleGerman;
            HeroImageUrl = heroImageUrl;
            AccountHolder = accountHolder;
            Iban = iban;
            BankName = bankName;
            ContentTurkish = contentTurkish;
            ContentGerman = contentGerman;
            CreatedAt = DateTime.UtcNow;
        }
        public static DonatePage CreateNew(Title heroTitleTurkish, Title heroTitleGerman,
            Title heroSubtitleTurkish, Title heroSubtitleGerman, Url heroImageUrl,
            string accountHolder, string iban, string bankName,
            string contentTurkish, string contentGerman)
        {
            return new DonatePage(Guid.NewGuid(), heroTitleTurkish, heroTitleGerman,
                heroSubtitleTurkish, heroSubtitleGerman, heroImageUrl,
                accountHolder, iban, bankName,
                contentTurkish, contentGerman);
        }
        public void Update(Title heroTitleTurkish, Title heroTitleGerman,
            Title heroSubtitleTurkish, Title heroSubtitleGerman, Url heroImageUrl,
            string accountHolder, string iban, string bankName,
            string contentTurkish, string contentGerman)
        {
            UpdateHeroTitle(heroTitleTurkish, heroTitleGerman);
            UpdateHeroSubTitle(heroSubtitleTurkish, heroSubtitleGerman);
            HeroImageUrl = heroImageUrl;
            AccountHolder = accountHolder;
            Iban = iban;
            BankName = bankName;
            ContentTurkish = contentTurkish;
            ContentGerman = contentGerman;
        }

        private void UpdateHeroTitle(Title heroTitleTurkish, Title heroTitleGerman)
        {
            HeroTitleTurkish = heroTitleTurkish;
            HeroTitleGerman = heroTitleGerman;
            SetUpdatedAt();
        }

        private void UpdateHeroSubTitle(Title heroSubtitleTurkish, Title heroSubtitleGerman)
        {
            HeroSubtitleTurkish = heroSubtitleTurkish;
            HeroSubtitleGerman = heroSubtitleGerman;
            SetUpdatedAt();
        }

    }
}
