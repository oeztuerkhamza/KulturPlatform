using FluentValidation;
using KulturPlatform.Application.Commands.DonatePage;

namespace KulturPlatform.Application.Validation.DonatePage
{
    public class CreateDonatePageValidator : AbstractValidator<CreateDonatePageCommand>
    {
        public CreateDonatePageValidator()
        {
            // Hero Section
            RuleFor(x => x.HeroTitleTr).NotEmpty().WithMessage("Hero Title (TR) bo? olamaz.");
            RuleFor(x => x.HeroTitleDe).NotEmpty().WithMessage("Hero Title (DE) bo? olamaz.");
            RuleFor(x => x.HeroSubtitleTr).NotEmpty().WithMessage("Hero Subtitle (TR) bo? olamaz.");
            RuleFor(x => x.HeroSubtitleDe).NotEmpty().WithMessage("Hero Subtitle (DE) bo? olamaz.");
            RuleFor(x => x.HeroImageUrl).NotEmpty().WithMessage("Hero Image URL bo? olamaz.");
            
            // Features
            RuleFor(x => x.Feature1TitleTr).NotEmpty().WithMessage("Feature 1 Title (TR) bo? olamaz.");
            RuleFor(x => x.Feature1TitleDe).NotEmpty().WithMessage("Feature 1 Title (DE) bo? olamaz.");
            RuleFor(x => x.Feature2TitleTr).NotEmpty().WithMessage("Feature 2 Title (TR) bo? olamaz.");
            RuleFor(x => x.Feature2TitleDe).NotEmpty().WithMessage("Feature 2 Title (DE) bo? olamaz.");
            RuleFor(x => x.Feature3TitleTr).NotEmpty().WithMessage("Feature 3 Title (TR) bo? olamaz.");
            RuleFor(x => x.Feature3TitleDe).NotEmpty().WithMessage("Feature 3 Title (DE) bo? olamaz.");
            
            // Why Donate Section
            RuleFor(x => x.WhyDonateTitleTr).NotEmpty().WithMessage("Why Donate Title (TR) bo? olamaz.");
            RuleFor(x => x.WhyDonateTitleDe).NotEmpty().WithMessage("Why Donate Title (DE) bo? olamaz.");
            RuleFor(x => x.WhyDonateDescriptionTr).NotEmpty().WithMessage("Why Donate Description (TR) bo? olamaz.");
            RuleFor(x => x.WhyDonateDescriptionDe).NotEmpty().WithMessage("Why Donate Description (DE) bo? olamaz.");
            
            // Where Section
            RuleFor(x => x.WhereTitleTr).NotEmpty().WithMessage("Where Title (TR) bo? olamaz.");
            RuleFor(x => x.WhereTitleDe).NotEmpty().WithMessage("Where Title (DE) bo? olamaz.");
            RuleFor(x => x.WhereDescriptionTr).NotEmpty().WithMessage("Where Description (TR) bo? olamaz.");
            RuleFor(x => x.WhereDescriptionDe).NotEmpty().WithMessage("Where Description (DE) bo? olamaz.");
            RuleFor(x => x.TaxInfoTr).NotEmpty().WithMessage("Tax Info (TR) bo? olamaz.");
            RuleFor(x => x.TaxInfoDe).NotEmpty().WithMessage("Tax Info (DE) bo? olamaz.");
            
            // Bank Details
            RuleFor(x => x.AccountHolder).NotEmpty().WithMessage("Account Holder bo? olamaz.");
            RuleFor(x => x.Iban).NotEmpty().WithMessage("IBAN bo? olamaz.");
            RuleFor(x => x.BicSwift).NotEmpty().WithMessage("BIC/SWIFT bo? olamaz.");
            RuleFor(x => x.BankName).NotEmpty().WithMessage("Bank Name bo? olamaz.");
            
            // PayPal Details
            RuleFor(x => x.PayPalUrl).NotEmpty().WithMessage("PayPal URL bo? olamaz.");
            RuleFor(x => x.PayPalHandle).NotEmpty().WithMessage("PayPal Handle bo? olamaz.");
            
            // Legacy Content
            RuleFor(x => x.ContentTr).NotEmpty().WithMessage("Content (TR) bo? olamaz.");
            RuleFor(x => x.ContentDe).NotEmpty().WithMessage("Content (DE) bo? olamaz.");
        }
    }
}
