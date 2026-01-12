using MediatR;
using System;

namespace KulturPlatform.Application.Commands.Home
{
    public record CreateFeatureCommand(
        string TitleTr,
        string TitleDe,
        string DescriptionTr,
        string DescriptionDe,
        string Color
    ) : IRequest<Guid>;
}
