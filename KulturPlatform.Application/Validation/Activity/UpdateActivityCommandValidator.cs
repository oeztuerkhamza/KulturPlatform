using FluentValidation;
using KulturPlatform.Application.Commands.Activity;

namespace KulturPlatform.Application.Validation.Activity
{
    public sealed class UpdateActivityCommandValidator
        : AbstractValidator<UpdateActivityCommand>
    {
        public UpdateActivityCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

            this.ApplyActivityRules(
                x => x.TitleTr,
                x => x.TitleDe,
                x => x.DescriptionTr,
                x => x.DescriptionDe,
                x => x.Date,
                x => x.Address,
                x => x.Category,
                x => x.ImageUrl,
                x => x.VideoUrl,
                x => x.GalleryImages,
                x => x.DetailedContentTr,
                x => x.DetailedContentDe
            );
        }
    }
}
