using FluentValidation;
using KulturPlatform.Application.Commands.ContactInfo;

namespace KulturPlatform.Application.Validation.ContactInfo
{
    public class UpdateContactInfoCommandValidator
        : AbstractValidator<UpdateContactInfoCommand>
    {
        public UpdateContactInfoCommandValidator()
        {
            RuleFor(x => x.Dto.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Dto.Phone).NotEmpty();
            RuleFor(x => x.Dto.Address).NotNull();
            RuleFor(x => x.Dto.Address.City).NotEmpty();
            RuleFor(x => x.Dto.Address.Country).NotEmpty();
        }
    }

}
