using AutoMapper;
using KulturPlatform.Application.Commands.AboutUs;
using KulturPlatform.Application.Dtos.AboutUs;
using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.Aggregates;
using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.AboutUs
{
    public class CreateOrUpdateAboutUsCommandHandler : IRequestHandler<CreateOrUpdateAboutUsCommand, Unit>
    {
        private readonly IAboutUsReadRepository _readRepo;
        private readonly IAboutUsWriteRepository _writeRepo;
        private readonly IUnitOfWork _uow;

        public CreateOrUpdateAboutUsCommandHandler(
            IAboutUsReadRepository readRepo,
            IAboutUsWriteRepository writeRepo,
            IUnitOfWork uow)
        {
            _readRepo = readRepo;
            _writeRepo = writeRepo;
            _uow = uow;
        }

        public async Task<Unit> Handle(CreateOrUpdateAboutUsCommand request, CancellationToken cancellationToken)
        {
            // singleton approach - get existing (first) if any
            var existing = await _readRepo.GetAsync(cancellationToken);

            // Build child collections
            var coreValues = request.Dto.CoreValues?.Select(x => CoreValue.Create(
                new Title(x.TitleTr?.Value ?? string.Empty),
                new Title(x.TitleDe?.Value ?? string.Empty),
                new Description(x.DescriptionTr?.Value ?? string.Empty),
                new Description(x.DescriptionDe?.Value ?? string.Empty),
                x.Order))
                .ToList() ?? new List<CoreValue>();

            var focusAreas = request.Dto.FocusAreas?.Select(x => FocusArea.Create(
                new Title(x.TitleTr?.Value ?? string.Empty),
                new Title(x.TitleDe?.Value ?? string.Empty),
                new Description(x.DescriptionTr?.Value ?? string.Empty),
                new Description(x.DescriptionDe?.Value ?? string.Empty),
                x.Order))
                .ToList() ?? new List<FocusArea>();

            var activityAreas = request.Dto.ActivityAreas?.Select(x => ActivityArea.Create(
                new Title(x.TitleTr?.Value ?? string.Empty),
                new Title(x.TitleDe?.Value ?? string.Empty),
                new Description(x.DescriptionTr?.Value ?? string.Empty),
                new Description(x.DescriptionDe?.Value ?? string.Empty),
                x.Order))
                .ToList() ?? new List<ActivityArea>();

            var teamMembers = request.Dto.TeamMembers?.Select(x => TeamMember.Create(
                new Name(x.Name?.Value ?? string.Empty),
                new Title(x.TitleTr?.Value ?? string.Empty),
                new Title(x.TitleDe?.Value ?? string.Empty),
                x.ImageUrl,
                x.Order))
                .ToList() ?? new List<TeamMember>();

            if (existing == null)
            {
                var about = AboutUs.Create(
                    new Description(request.Dto.Quote?.Value ?? string.Empty),
                    request.Dto.QuoteAuthor ?? string.Empty,
                    new Description(request.Dto.WhoWeAreTr?.Value ?? string.Empty),
                    new Description(request.Dto.WhoWeAreDe?.Value ?? string.Empty),
                    new Description(request.Dto.GoalsTr?.Value ?? string.Empty),
                    new Description(request.Dto.GoalsDe?.Value ?? string.Empty),
                    new Description(request.Dto.VisionTr?.Value ?? string.Empty),
                    new Description(request.Dto.VisionDe?.Value ?? string.Empty),
                    new Description(request.Dto.MissionTr?.Value ?? string.Empty),
                    new Description(request.Dto.MissionDe?.Value ?? string.Empty),
                    coreValues, focusAreas, activityAreas, teamMembers);

                await _writeRepo.AddAsync(about, cancellationToken);
                await _uow.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }

            existing.Update(
                new Description(request.Dto.Quote?.Value ?? string.Empty),
                request.Dto.QuoteAuthor ?? string.Empty,
                new Description(request.Dto.WhoWeAreTr?.Value ?? string.Empty),
                new Description(request.Dto.WhoWeAreDe?.Value ?? string.Empty),
                new Description(request.Dto.GoalsTr?.Value ?? string.Empty),
                new Description(request.Dto.GoalsDe?.Value ?? string.Empty),
                new Description(request.Dto.VisionTr?.Value ?? string.Empty),
                new Description(request.Dto.VisionDe?.Value ?? string.Empty),
                new Description(request.Dto.MissionTr?.Value ?? string.Empty),
                new Description(request.Dto.MissionDe?.Value ?? string.Empty),
                coreValues, focusAreas, activityAreas, teamMembers);

            await _writeRepo.UpdateAsync(existing, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
