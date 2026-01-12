using KulturPlatform.Application.Interfaces.AboutUs;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Application.Commands.AboutUs
{
    public class UpdateAboutUsCommandHandler
        : IRequestHandler<UpdateAboutUsCommand, Unit>
    {
        private readonly IAboutUsWriteRepository _repo;
        private readonly IUnitOfWork _uow;

        public UpdateAboutUsCommandHandler(
            IAboutUsWriteRepository repo,
            IUnitOfWork uow)
        {
            _repo = repo;
            _uow = uow;
        }

        public async Task<Unit> Handle(
            UpdateAboutUsCommand request,
            CancellationToken ct)
        {
            var aboutUs = await _repo.GetAsync(ct);

            if (aboutUs == null)
                throw new KeyNotFoundException("AboutUs record not found.");

            void ApplyChangesTo(Domain.Commons.Aggregates.AboutUs target)
            {
                target.Update(
                    new Description(request.Model.QuoteTr.Value),
                    new Description(request.Model.QuoteDe.Value),
                    request.Model.QuoteAuthor,
                    new Description(request.Model.WhoWeAreTr.Value),
                    new Description(request.Model.WhoWeAreDe.Value),
                    new Description(request.Model.GoalsTr.Value),
                    new Description(request.Model.GoalsDe.Value),
                    new Description(request.Model.VisionTr.Value),
                    new Description(request.Model.VisionDe.Value),
                    new Description(request.Model.MissionTr.Value),
                    new Description(request.Model.MissionDe.Value),
                    request.Model.CoreValues?.Select(x => Domain.Commons.Entities.CoreValue.Create(
                        new Title(x.TitleTr.Value),
                        new Title(x.TitleDe.Value),
                        new Description(x.DescriptionTr.Value),
                        new Description(x.DescriptionDe.Value),
                        x.Order)) ?? Enumerable.Empty<Domain.Commons.Entities.CoreValue>(),
                    request.Model.FocusAreas?.Select(x => Domain.Commons.Entities.FocusArea.Create(
                        new Title(x.TitleTr.Value),
                        new Title(x.TitleDe.Value),
                        new Description(x.DescriptionTr.Value),
                        new Description(x.DescriptionDe.Value),
                        x.Order)) ?? Enumerable.Empty<Domain.Commons.Entities.FocusArea>(),
                    request.Model.ActivityAreas?.Select(x => Domain.Commons.Entities.ActivityArea.Create(
                        new Title(x.TitleTr.Value),
                        new Title(x.TitleDe.Value),
                        new Description(x.DescriptionTr.Value),
                        new Description(x.DescriptionDe.Value),
                        x.Order)) ?? Enumerable.Empty<Domain.Commons.Entities.ActivityArea>(),
                    request.Model.TeamMembers?.Select(x => Domain.Commons.Entities.TeamMember.Create(
                        new Name(x.Name.Value),
                        new Title(x.TitleTr.Value),
                        new Title(x.TitleDe.Value),
                        x.ImageUrl,
                        x.Order)) ?? Enumerable.Empty<Domain.Commons.Entities.TeamMember>()
                );
            }

            // Apply changes to loaded entity and mark modified
            ApplyChangesTo(aboutUs);
            await _repo.UpdateAsync(aboutUs, ct);

            try
            {
                await _uow.SaveChangesAsync(ct);
                return Unit.Value;
            }
            catch (DbUpdateConcurrencyException)
            {
                // Retry once: reload current DB state, reapply changes, save again
                var fresh = await _repo.GetAsync(ct);
                if (fresh == null)
                    throw new InvalidOperationException("Concurrency conflict: AboutUs was deleted by another process.");

                ApplyChangesTo(fresh);
                await _repo.UpdateAsync(fresh, ct);

                try
                {
                    await _uow.SaveChangesAsync(ct);
                    return Unit.Value;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw new InvalidOperationException("Concurrency conflict when updating AboutUs after retry. The record may have been modified by another user.", ex);
                }
            }
        }
    }
}
