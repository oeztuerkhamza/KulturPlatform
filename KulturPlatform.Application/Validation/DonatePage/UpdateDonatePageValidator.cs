using FluentValidation;
using KulturPlatform.Application.Commands.DonatePage;

namespace KulturPlatform.Application.Validation.DonatePage
{
    public class UpdateDonatePageValidator : AbstractValidator<UpdateDonatePageCommand>
    {
        public UpdateDonatePageValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id boş olamaz.");
            
            // Hero Section
            RuleFor(x => x.HeroTitleTr).NotEmpty().WithMessage("Hero Title (TR) boş olamaz.");
            RuleFor(x => x.HeroTitleDe).NotEmpty().WithMessage("Hero Title (DE) boş olamaz.");
            RuleFor(x => x.HeroSubtitleTr).NotEmpty().WithMessage("Hero Subtitle (TR) boş olamaz.");
            RuleFor(x => x.HeroSubtitleDe).NotEmpty().WithMessage("Hero Subtitle (DE) boş olamaz.");
            RuleFor(x => x.HeroImageUrl).NotEmpty().WithMessage("Hero Image URL boş olamaz.");
            
            // Features
            RuleFor(x => x.Feature1TitleTr).NotEmpty().WithMessage("Feature 1 Title (TR) boş olamaz.");
            RuleFor(x => x.Feature1TitleDe).NotEmpty().WithMessage("Feature 1 Title (DE) boş olamaz.");
            RuleFor(x => x.Feature2TitleTr).NotEmpty().WithMessage("Feature 2 Title (TR) boş olamaz.");
            RuleFor(x => x.Feature2TitleDe).NotEmpty().WithMessage("Feature 2 Title (DE) boş olamaz.");
            RuleFor(x => x.Feature3TitleTr).NotEmpty().WithMessage("Feature 3 Title (TR) boş olamaz.");
            RuleFor(x => x.Feature3TitleDe).NotEmpty().WithMessage("Feature 3 Title (DE) boş olamaz.");
            
            // Why Donate Section
            RuleFor(x => x.WhyDonateTitleTr).NotEmpty().WithMessage("Why Donate Title (TR) boş olamaz.");
            RuleFor(x => x.WhyDonateTitleDe).NotEmpty().WithMessage("Why Donate Title (DE) boş olamaz.");
            RuleFor(x => x.WhyDonateDescriptionTr).NotEmpty().WithMessage("Why Donate Description (TR) boş olamaz.");
            RuleFor(x => x.WhyDonateDescriptionDe).NotEmpty().WithMessage("Why Donate Description (DE) boş olamaz.");
            
            // Where Section
            RuleFor(x => x.WhereTitleTr).NotEmpty().WithMessage("Where Title (TR) boş olamaz.");
            RuleFor(x => x.WhereTitleDe).NotEmpty().WithMessage("Where Title (DE) boş olamaz.");
            RuleFor(x => x.WhereDescriptionTr).NotEmpty().WithMessage("Where Description (TR) boş olamaz.");
            RuleFor(x => x.WhereDescriptionDe).NotEmpty().WithMessage("Where Description (DE) boş olamaz.");
            RuleFor(x => x.TaxInfoTr).NotEmpty().WithMessage("Tax Info (TR) boş olamaz.");
            RuleFor(x => x.TaxInfoDe).NotEmpty().WithMessage("Tax Info (DE) boş olamaz.");
            
            // Bank Details
            RuleFor(x => x.AccountHolder).NotEmpty().WithMessage("Account Holder boş olamaz.");
            RuleFor(x => x.Iban).NotEmpty().WithMessage("IBAN boş olamaz.");
            RuleFor(x => x.BicSwift).NotEmpty().WithMessage("BIC/SWIFT boş olamaz.");
            RuleFor(x => x.BankName).NotEmpty().WithMessage("Bank Name boş olamaz.");
            
            // PayPal Details
            RuleFor(x => x.PayPalUrl).NotEmpty().WithMessage("PayPal URL boş olamaz.");
            RuleFor(x => x.PayPalHandle).NotEmpty().WithMessage("PayPal Handle boş olamaz.");
            
            // Legacy Content
            RuleFor(x => x.ContentTr).NotEmpty().WithMessage("Content (TR) boş olamaz.");
            RuleFor(x => x.ContentDe).NotEmpty().WithMessage("Content (DE) boş olamaz.");
        }
    }
}
