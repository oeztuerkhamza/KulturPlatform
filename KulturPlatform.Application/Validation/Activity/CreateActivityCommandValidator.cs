using FluentValidation;
using KulturPlatform.Application.Commands.Activity;

namespace KulturPlatform.Application.Validation.Activity
{
    public sealed class CreateActivityCommandValidator
        : AbstractValidator<CreateActivityCommand>
    {
        public CreateActivityCommandValidator()
        {
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
