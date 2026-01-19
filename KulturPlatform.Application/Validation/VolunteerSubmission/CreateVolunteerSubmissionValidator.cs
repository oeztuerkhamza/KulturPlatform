using FluentValidation;
using KulturPlatform.Application.Commands.VolunteerSubmission;

namespace KulturPlatform.Application.Validation.VolunteerSubmission
{
    public class CreateVolunteerSubmissionValidator : AbstractValidator<CreateVolunteerSubmissionCommand>
    {
        public CreateVolunteerSubmissionValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MinimumLength(3).WithMessage("Full name must be at least 3 characters.")
                .MaximumLength(200).WithMessage("Full name cannot exceed 200 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.PhoneNumber)
                .Must(phone => string.IsNullOrWhiteSpace(phone) || phone.Length >= 7)
                .WithMessage("Phone number must be at least 7 characters if provided.")
                .Must(phone => string.IsNullOrWhiteSpace(phone) || phone.Length <= 20)
                .WithMessage("Phone number cannot exceed 20 characters.");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Message is required.")
                .MinimumLength(10).WithMessage("Message must be at least 10 characters.")
                .MaximumLength(2000).WithMessage("Message cannot exceed 2000 characters.");
        }
    }
}
