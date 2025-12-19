using FluentValidation;
using KulturPlatform.Application.Commands.AboutUs;

namespace KulturPlatform.Application.Validation.AboutUs
{
    public class UpdateAboutUsCommandValidator
        : AbstractValidator<UpdateAboutUsCommand>
    {
        public UpdateAboutUsCommandValidator()
        {
            RuleFor(x => x.Model.WhoWeAreTr).NotEmpty();
            RuleFor(x => x.Model.WhoWeAreDe).NotEmpty(); ;

            RuleForEach(x => x.Model.TeamMembers).ChildRules(tm =>
            {
                tm.RuleFor(x => x.Name.Value).NotEmpty();
                tm.RuleFor(x => x.TitleTr.Value).NotEmpty();
                tm.RuleFor(x => x.TitleDe.Value).NotEmpty();
                tm.RuleFor(x => x.ImageUrl).NotEmpty();
            });
        }
    }

}
