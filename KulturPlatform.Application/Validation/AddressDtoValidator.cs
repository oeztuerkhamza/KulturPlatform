using FluentValidation;
using KulturPlatform.Application.Dtos.LocalizationDto;

namespace KulturPlatform.Application.Validation
{
    public class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(x => x.Street)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.HouseNo)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.ZipCode)
                .NotEmpty()
                .MaximumLength(10);

            RuleFor(x => x.City)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.State)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Country)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
