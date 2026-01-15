using MediatR;

namespace KulturPlatform.Application.Commands.DonatePage
{
    public record CreateDonatePageCommand(
        // Hero Section
        string HeroTitleTr,
        string HeroTitleDe,
        string HeroSubtitleTr,
        string HeroSubtitleDe,
        string HeroImageUrl,
        // Feature Highlights
        string Feature1TitleTr,
        string Feature1TitleDe,
        string Feature2TitleTr,
        string Feature2TitleDe,
        string Feature3TitleTr,
        string Feature3TitleDe,
        // Why Donate Section
        string WhyDonateTitleTr,
        string WhyDonateTitleDe,
        string WhyDonateDescriptionTr,
        string WhyDonateDescriptionDe,
        // Where Section
        string WhereTitleTr,
        string WhereTitleDe,
        string WhereDescriptionTr,
        string WhereDescriptionDe,
        string TaxInfoTr,
        string TaxInfoDe,
        // Bank Account Details
        string AccountHolder,
        string Iban,
        string BicSwift,
        string BankName,
        // PayPal Details
        string PayPalUrl,
        string PayPalHandle,
        // Legacy Content
        string ContentTr,
        string ContentDe
    ) : IRequest<Guid>;
}
