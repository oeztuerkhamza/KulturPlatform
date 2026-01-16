using FluentValidation;
using KulturPlatform.Application.Commands.GuelenMovement;

namespace KulturPlatform.Application.Validation.GuelenMovement
{
    public class UpdateGuelenMovementValidator : AbstractValidator<UpdateGuelenMovementCommand>
    {
        public UpdateGuelenMovementValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id bo? olamaz.");
            
            // Main Content
            RuleFor(x => x.TitleTr).NotEmpty().WithMessage("Ba?l?k (TR) bo? olamaz.");
            RuleFor(x => x.TitleDe).NotEmpty().WithMessage("Ba?l?k (DE) bo? olamaz.");
            RuleFor(x => x.IntroductionTr).NotEmpty().WithMessage("Giri? (TR) bo? olamaz.");
            RuleFor(x => x.IntroductionDe).NotEmpty().WithMessage("Giri? (DE) bo? olamaz.");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Görsel URL bo? olamaz.");

            // Philosophy Section
            RuleFor(x => x.PhilosophyTitleTr).NotEmpty().WithMessage("Felsefe Ba?l??? (TR) bo? olamaz.");
            RuleFor(x => x.PhilosophyTitleDe).NotEmpty().WithMessage("Felsefe Ba?l??? (DE) bo? olamaz.");
            RuleFor(x => x.PhilosophyContentTr).NotEmpty().WithMessage("Felsefe ?çeri?i (TR) bo? olamaz.");
            RuleFor(x => x.PhilosophyContentDe).NotEmpty().WithMessage("Felsefe ?çeri?i (DE) bo? olamaz.");

            // Dialog Section
            RuleFor(x => x.DialogTitleTr).NotEmpty().WithMessage("Diyalog Ba?l??? (TR) bo? olamaz.");
            RuleFor(x => x.DialogTitleDe).NotEmpty().WithMessage("Diyalog Ba?l??? (DE) bo? olamaz.");
            RuleFor(x => x.DialogContentTr).NotEmpty().WithMessage("Diyalog ?çeri?i (TR) bo? olamaz.");
            RuleFor(x => x.DialogContentDe).NotEmpty().WithMessage("Diyalog ?çeri?i (DE) bo? olamaz.");

            // Network Section
            RuleFor(x => x.NetworkTitleTr).NotEmpty().WithMessage("A? Ba?l??? (TR) bo? olamaz.");
            RuleFor(x => x.NetworkTitleDe).NotEmpty().WithMessage("A? Ba?l??? (DE) bo? olamaz.");
            RuleFor(x => x.NetworkContentTr).NotEmpty().WithMessage("A? ?çeri?i (TR) bo? olamaz.");
            RuleFor(x => x.NetworkContentDe).NotEmpty().WithMessage("A? ?çeri?i (DE) bo? olamaz.");

            // Spiritual Section
            RuleFor(x => x.SpiritualTitleTr).NotEmpty().WithMessage("Manevi Kökler Ba?l??? (TR) bo? olamaz.");
            RuleFor(x => x.SpiritualTitleDe).NotEmpty().WithMessage("Manevi Kökler Ba?l??? (DE) bo? olamaz.");
            RuleFor(x => x.SpiritualContentTr).NotEmpty().WithMessage("Manevi Kökler ?çeri?i (TR) bo? olamaz.");
            RuleFor(x => x.SpiritualContentDe).NotEmpty().WithMessage("Manevi Kökler ?çeri?i (DE) bo? olamaz.");

            // Vision Section
            RuleFor(x => x.VisionTitleTr).NotEmpty().WithMessage("Vizyon Ba?l??? (TR) bo? olamaz.");
            RuleFor(x => x.VisionTitleDe).NotEmpty().WithMessage("Vizyon Ba?l??? (DE) bo? olamaz.");
            RuleFor(x => x.VisionContentTr).NotEmpty().WithMessage("Vizyon ?çeri?i (TR) bo? olamaz.");
            RuleFor(x => x.VisionContentDe).NotEmpty().WithMessage("Vizyon ?çeri?i (DE) bo? olamaz.");
        }
    }
}
