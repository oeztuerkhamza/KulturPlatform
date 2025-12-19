using KulturPlatform.Application.Interfaces.Imprint;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.Imprint
{
    public class CreateImprintHandler : IRequestHandler<CreateImprintCommand, Guid>
    {
        private readonly IImprintRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateImprintHandler(IImprintRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateImprintCommand request, CancellationToken cancellationToken)
        {
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

            var imprint = Domain.Commons.Aggregates.Imprint.CreateNew(
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

            await _repository.AddAsync(imprint, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return imprint.Id;
        }
    }
}
