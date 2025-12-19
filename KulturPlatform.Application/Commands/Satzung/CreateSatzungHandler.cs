using KulturPlatform.Application.Interfaces.Satzung;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.Satzung
{
    public class CreateSatzungHandler : IRequestHandler<CreateSatzungCommand, Guid>
    {
        private readonly ISatzungRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSatzungHandler(ISatzungRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateSatzungCommand request, CancellationToken cancellationToken)
        {
            // Purposes VO dönüştürme
            var purposes = request.Purposes?.Select(p =>
                Purpose.Create(p.Letter, p.Content)
            ).ToList() ?? new List<Purpose>();

            // Membership VO dönüştürme
            var memberships = request.Memberships?.Select(m =>
                MembershipDetail.Create(m.Type, m.DescriptionTurkish, m.DescriptionGerman)
            ).ToList() ?? new List<MembershipDetail>();

            var satzung = Domain.Commons.Aggregates.Satzung.CreateNew(
                request.Key,
                request.TitleTurkish,
                request.TitleGerman,
                request.NameAndSeatTurkish,
                request.NameAndSeatGerman,
                request.NameDescTurkish,
                request.NameDescGerman,
                request.SeatTurkish,
                request.SeatGerman,
                request.SeatDescTurkish,
                request.SeatDescGerman,
                request.FiscalYearTurkish,
                request.FiscalYearGerman,
                request.FiscalYearDescTurkish,
                request.FiscalYearDescGerman,
                request.PurposeOfAssociationTurkish,
                request.PurposeOfAssociationGerman,
                purposes,
                request.GemeinnuetzigkeitTurkish,
                request.GemeinnuetzigkeitGerman,
                request.PoliticalNeutralityTurkish,
                request.PoliticalNeutralityGerman,
                memberships
            );

            await _repository.AddAsync(satzung, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return satzung.Id;
        }
    }
}
