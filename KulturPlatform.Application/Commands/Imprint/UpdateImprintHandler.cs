using KulturPlatform.Application.Interfaces.Imprint;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.Imprint
{
    public class UpdateImprintHandler : IRequestHandler<UpdateImprintCommand>
    {
        private readonly IImprintRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateImprintHandler(IImprintRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateImprintCommand request, CancellationToken cancellationToken)
        {
            var imprint = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (imprint == null)
                throw new KeyNotFoundException($"Imprint with Id {request.Id} not found.");

            var addressDto = request.Dto.Address
                             ?? throw new ArgumentNullException(nameof(request.Dto.Address));

            var address = new Address(
                addressDto.Street,
                addressDto.HouseNo,
                addressDto.ZipCode,
                addressDto.City,
                addressDto.State,
                addressDto.Country

            );
            var email = new Email(request.Dto.Email);
            var phone = new PhoneNumber(request.Dto.Phone);
            var president = new Name(request.Dto.President);
            var vicePresident = new Name(request.Dto.VicePresident);
            var organizationName = Title.Create(request.Dto.OrganizationName);

            imprint.Update(
                organizationName,
                request.Dto.OrganizationType,
                address,
                email,
                phone,
                president,
                vicePresident,
                request.Dto.LegalStructureTurkish,
                request.Dto.LegalStructureGerman,
                request.Dto.PurposeTurkish,
                request.Dto.PurposeGerman,
                request.Dto.TaxExemptionTurkish,
                request.Dto.TaxExemptionGerman,
                request.Dto.ContentResponsibilityTurkish,
                request.Dto.ContentResponsibilityGerman,
                request.Dto.LinksResponsibilityTurkish,
                request.Dto.LinksResponsibilityGerman,
                request.Dto.CopyrightTurkish,
                request.Dto.CopyrightGerman
            );
            _repository.Update(imprint, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
