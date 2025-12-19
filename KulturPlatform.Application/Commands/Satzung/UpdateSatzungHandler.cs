using KulturPlatform.Application.Interfaces.Satzung;
using KulturPlatform.Application.NewFolder;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.Satzung
{
    public class UpdateSatzungHandler : IRequestHandler<UpdateSatzungCommand>
    {
        private readonly ISatzungRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSatzungHandler(ISatzungRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateSatzungCommand request, CancellationToken cancellationToken)
        {
            var satzung = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (satzung is null)
                throw new SatzungNotFoundException(request.Id);

            // ❗ Command zaten domain ValueObject içeriyor
            satzung.Update(
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
                request.Purposes,
                request.GemeinnuetzigkeitTurkish,
                request.GemeinnuetzigkeitGerman,
                request.PoliticalNeutralityTurkish,
                request.PoliticalNeutralityGerman,
                request.Memberships
            );

            await _unitOfWork.SaveChangesAsync(cancellationToken);

        }
    }
}
