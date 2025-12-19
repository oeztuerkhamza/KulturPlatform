using FluentValidation;
using KulturPlatform.Application.Commands.DonatePage;

namespace KulturPlatform.Application.Validation.DonatePage
{
    public class UpdateDonatePageValidator : AbstractValidator<UpdateDonatePageCommand>
    {
        public UpdateDonatePageValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id boş olamaz.");
            RuleFor(x => x.HeroTitleTr).NotEmpty().WithMessage("Hero Title (TR) boş olamaz.");
            RuleFor(x => x.HeroTitleDe).NotEmpty().WithMessage("Hero Title (DE) boş olamaz.");
            RuleFor(x => x.HeroSubtitleTr).NotEmpty().WithMessage("Hero Subtitle (TR) boş olamaz.");
            RuleFor(x => x.HeroSubtitleDe).NotEmpty().WithMessage("Hero Subtitle (DE) boş olamaz.");
            RuleFor(x => x.HeroImageUrl).NotEmpty().WithMessage("Hero Image URL boş olamaz.");
            RuleFor(x => x.AccountHolder).NotEmpty().WithMessage("Account Holder boş olamaz.");
            RuleFor(x => x.Iban).NotEmpty().WithMessage("IBAN boş olamaz.");
            RuleFor(x => x.BankName).NotEmpty().WithMessage("Bank Name boş olamaz.");
            RuleFor(x => x.ContentTr).NotEmpty().WithMessage("Content (TR) boş olamaz.");
            RuleFor(x => x.ContentDe).NotEmpty().WithMessage("Content (DE) boş olamaz.");
        }
    }

}
