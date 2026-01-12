using KulturPlatform.Application.Dtos;
using KulturPlatform.Application.Dtos.AboutUs;
using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs
{
    public sealed class CreateOrUpdateAboutUsCommandHandler
    : IRequestHandler<CreateOrUpdateAboutUsCommand, Unit>
    {
        private readonly IAboutUsReadRepository _readRepo;
        private readonly IAboutUsWriteRepository _writeRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrUpdateAboutUsCommandHandler(
            IAboutUsReadRepository readRepo,
            IAboutUsWriteRepository writeRepo,
            IUnitOfWork unitOfWork)
        {
            _readRepo = readRepo;
            _writeRepo = writeRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(
            CreateOrUpdateAboutUsCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var aboutUs = await _readRepo.GetAsync(cancellationToken);

            var coreValues = dto.CoreValues?.Select(ToCoreValue).ToList() ?? [];
            var focusAreas = dto.FocusAreas?.Select(ToFocusArea).ToList() ?? [];
            var activityAreas = dto.ActivityAreas?.Select(ToActivityArea).ToList() ?? [];
            var teamMembers = dto.TeamMembers?.Select(ToTeamMember).ToList() ?? [];

            if (aboutUs is null)
            {
                aboutUs = Domain.Commons.Aggregates.AboutUs.Create(
                    Desc(dto.QuoteTr),
                    Desc(dto.QuoteDe),
                    dto.QuoteAuthor ?? string.Empty,
                    Desc(dto.WhoWeAreTr),
                    Desc(dto.WhoWeAreDe),
                    Desc(dto.GoalsTr),
                    Desc(dto.GoalsDe),
                    Desc(dto.VisionTr),
                    Desc(dto.VisionDe),
                    Desc(dto.MissionTr),
                    Desc(dto.MissionDe),
                    coreValues,
                    focusAreas,
                    activityAreas,
                    teamMembers
                );

                await _writeRepo.AddAsync(aboutUs, cancellationToken);
            }
            else
            {
                aboutUs.Update(
                    Desc(dto.QuoteTr),
                    Desc(dto.QuoteDe),
                    dto.QuoteAuthor ?? string.Empty,
                    Desc(dto.WhoWeAreTr),
                    Desc(dto.WhoWeAreDe),
                    Desc(dto.GoalsTr),
                    Desc(dto.GoalsDe),
                    Desc(dto.VisionTr),
                    Desc(dto.VisionDe),
                    Desc(dto.MissionTr),
                    Desc(dto.MissionDe),
                    coreValues,
                    focusAreas,
                    activityAreas,
                    teamMembers
                );

                await _writeRepo.UpdateAsync(aboutUs, cancellationToken);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        // ---------- Mapping Helpers ----------

        private static Description Desc(DescriptionDto dto)
            => new(dto?.Value ?? string.Empty);

        private static CoreValue ToCoreValue(AboutUsItemDto x)
            => CoreValue.Create(
                new Title(x.TitleTr?.Value ?? string.Empty),
                new Title(x.TitleDe?.Value ?? string.Empty),
                new Description(x.DescriptionTr?.Value ?? string.Empty),
                new Description(x.DescriptionDe?.Value ?? string.Empty),
                x.Order);

        private static FocusArea ToFocusArea(AboutUsItemDto x)
            => FocusArea.Create(
                new Title(x.TitleTr?.Value ?? string.Empty),
                new Title(x.TitleDe?.Value ?? string.Empty),
                new Description(x.DescriptionTr?.Value ?? string.Empty),
                new Description(x.DescriptionDe?.Value ?? string.Empty),
                x.Order);

        private static ActivityArea ToActivityArea(AboutUsItemDto x)
            => ActivityArea.Create(
                new Title(x.TitleTr?.Value ?? string.Empty),
                new Title(x.TitleDe?.Value ?? string.Empty),
                new Description(x.DescriptionTr?.Value ?? string.Empty),
                new Description(x.DescriptionDe?.Value ?? string.Empty),
                x.Order);

        private static TeamMember ToTeamMember(TeamMemberDto x)
            => TeamMember.Create(
                new Name(x.Name?.Value ?? string.Empty),
                new Title(x.TitleTr?.Value ?? string.Empty),
                new Title(x.TitleDe?.Value ?? string.Empty),
                x.ImageUrl,
                x.Order);
    }

}

