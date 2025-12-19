using FluentValidation;
using KulturPlatform.Application.Commands.Satzung;

namespace KulturPlatform.Application.Validation.Satzung
{
    public class UpdateSatzungValidator : AbstractValidator<UpdateSatzungCommand>
    {
        public UpdateSatzungValidator()
        {

            // Title
            RuleFor(x => x.TitleTurkish)
                .NotNull().WithMessage("Title (TR) boş olamaz.")
                .Must(t => !string.IsNullOrWhiteSpace(t.Value)).WithMessage("Title (TR) değeri boş olamaz.");

            RuleFor(x => x.TitleGerman)
                .NotNull().WithMessage("Title (DE) boş olamaz.")
                .Must(t => !string.IsNullOrWhiteSpace(t.Value)).WithMessage("Title (DE) değeri boş olamaz.");

            // Name & Seat
            RuleFor(x => x.NameAndSeatTurkish)
                .NotNull().WithMessage("Name & Seat (TR) boş olamaz.")
                .Must(sc => !string.IsNullOrWhiteSpace(sc.Heading) || !string.IsNullOrWhiteSpace(sc.BodyTurkish))
                .WithMessage("Name & Seat (TR) Heading veya Body boş olamaz.");

            RuleFor(x => x.NameAndSeatGerman)
                .NotNull().WithMessage("Name & Seat (DE) boş olamaz.")
                .Must(sc => !string.IsNullOrWhiteSpace(sc.Heading) || !string.IsNullOrWhiteSpace(sc.BodyGerman))
                .WithMessage("Name & Seat (DE) Heading veya Body boş olamaz.");

            // Purpose
            RuleFor(x => x.Purposes)
                .NotNull().WithMessage("Purposes listesi boş olamaz.")
                .Must(list => list.Count > 0).WithMessage("En az bir Purpose eklenmelidir.");

            // Memberships
            RuleForEach(x => x.Memberships).ChildRules(m =>
            {
                m.RuleFor(mem => mem.Type)
                    .NotNull().WithMessage("Membership Type boş olamaz.")
                    .Must(sc => !string.IsNullOrWhiteSpace(sc.Heading) ||
                               !string.IsNullOrWhiteSpace(sc.BodyTurkish) ||
                               !string.IsNullOrWhiteSpace(sc.BodyGerman))
                    .WithMessage("Membership Type Heading veya Body boş olamaz.");

                m.RuleFor(mem => mem.DescriptionTurkish)
                    .NotNull().WithMessage("Membership Description (TR) boş olamaz.")
                    .Must(sc => !string.IsNullOrWhiteSpace(sc.Heading) ||
                               !string.IsNullOrWhiteSpace(sc.BodyTurkish))
                    .WithMessage("Membership Description (TR) Heading veya BodyTurkish boş olamaz.");

                m.RuleFor(mem => mem.DescriptionGerman)
                    .NotNull().WithMessage("Membership Description (DE) boş olamaz.")
                    .Must(sc => !string.IsNullOrWhiteSpace(sc.Heading) ||
                               !string.IsNullOrWhiteSpace(sc.BodyGerman))
                    .WithMessage("Membership Description (DE) Heading veya BodyGerman boş olamaz.");
            });

            // Diğer SectionContent alanlarını da benzer şekilde ekleyebilirsin
            RuleFor(x => x.SeatTurkish)
                .NotNull().WithMessage("Seat (TR) boş olamaz.");

            RuleFor(x => x.SeatGerman)
                .NotNull().WithMessage("Seat (DE) boş olamaz.");

            RuleFor(x => x.PurposeOfAssociationTurkish)
                .NotNull().WithMessage("Purpose Of Association (TR) boş olamaz.");

            RuleFor(x => x.PurposeOfAssociationGerman)
                .NotNull().WithMessage("Purpose Of Association (DE) boş olamaz.");
        }
    }
}