using FluentValidation;
using KulturPlatform.Application.Commands.TeaEvent;

namespace KulturPlatform.Application.Validation.TeaEvent
{
    public class DeleteTeaEventValidator : AbstractValidator<DeleteTeaEventCommand>
    {
        public DeleteTeaEventValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id boş olamaz.");
        }
    }
}
