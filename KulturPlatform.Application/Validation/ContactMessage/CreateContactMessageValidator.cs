using FluentValidation;
using KulturPlatform.Application.Commands.ContactMessages;

namespace KulturPlatform.Application.Validation.ContactMessages
{
    public class CreateContactMessageValidator : AbstractValidator<CreateContactMessageCommand>
    {
        public CreateContactMessageValidator()
        {
            RuleFor(x => x.Anrede)
                .Must(anrede => string.IsNullOrWhiteSpace(anrede) ||
                               new[] { "Herr", "Frau", "Divers", "Bay", "Bayan", "Diğer" }.Contains(anrede, StringComparer.OrdinalIgnoreCase))
                .WithMessage("Anrede must be 'Herr', 'Frau', 'Divers', 'Bay', 'Bayan' or 'Diğer' if provided.");

            RuleFor(x => x.SenderName)
                .NotEmpty().WithMessage("Sender name is required.")
                .MinimumLength(3).WithMessage("Sender name must be at least 3 characters.")
                .MaximumLength(200).WithMessage("Sender name cannot exceed 200 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(200).WithMessage("Email cannot exceed 200 characters.");

            RuleFor(x => x.Phone)
                .Must(phone => string.IsNullOrWhiteSpace(phone) || phone.Length >= 7)
                .WithMessage("Phone number must be at least 7 characters if provided.")
                .Must(phone => string.IsNullOrWhiteSpace(phone) || phone.Length <= 20)
                .WithMessage("Phone number cannot exceed 20 characters.");

            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Subject is required.")
                .MinimumLength(3).WithMessage("Subject must be at least 3 characters.")
                .MaximumLength(200).WithMessage("Subject cannot exceed 200 characters.");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Message is required.")
                .MinimumLength(10).WithMessage("Message must be at least 10 characters.")
                .MaximumLength(5000).WithMessage("Message cannot exceed 5000 characters.");
        }
    }
}
