using KulturPlatform.Application.Interfaces.ContactInfo;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.ContactInfo
{
    public class CreateContactInfoCommandHandler : IRequestHandler<CreateContactInfoCommand, Guid>
    {
        private readonly IContactInfoRepository _contactInfoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateContactInfoCommandHandler(IContactInfoRepository contactInfoRepository, IUnitOfWork unitOfWork)
        {
            _contactInfoRepository = contactInfoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateContactInfoCommand request, CancellationToken cancellationToken)
        {
            var email = new Email(request.Dto.Email);
            var phone = new PhoneNumber(request.Dto.Phone);

            // Parse street string or use full format
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

            var socialMediaDto = request.Dto.SocialMedia;

            var socialMedia = socialMediaDto is null
                ? SocialMediaLinks.Empty
                : new SocialMediaLinks(
                    socialMediaDto.Facebook,
                    socialMediaDto.Instagram,
                    socialMediaDto.Twitter
                );

            var officeHours = request.Dto.OfficeHours;

            var contactInfo = Domain.Commons.Aggregates.ContactInfo.Create(
                email,
                phone,
                address,
                socialMedia,
                officeHours
            );

            await _contactInfoRepository.AddAsync(contactInfo, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return contactInfo.Id;
        }
    }
}
