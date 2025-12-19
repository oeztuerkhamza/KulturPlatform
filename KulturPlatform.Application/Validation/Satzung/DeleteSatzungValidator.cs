using FluentValidation;
using KulturPlatform.Application.Commands.Satzung;

namespace KulturPlatform.Application.Validation.Satzung
{
    public class DeleteSatzungValidator : AbstractValidator<DeleteSatzungCommand>
    {
        public DeleteSatzungValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id boş olamaz.");
        }
    }
}
