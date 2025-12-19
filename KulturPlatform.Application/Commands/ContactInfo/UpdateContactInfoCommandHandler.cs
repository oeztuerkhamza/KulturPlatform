using KulturPlatform.Application.Interfaces.ContactInfo;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;
using MediatR;

namespace KulturPlatform.Application.Commands.ContactInfo
{
    public class UpdateContactInfoCommandHandler : IRequestHandler<UpdateContactInfoCommand>
    {
        private readonly IContactInfoRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateContactInfoCommandHandler(IContactInfoRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateContactInfoCommand request, CancellationToken cancellationToken)
        {
            var contactInfo = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (contactInfo == null)
                throw new KeyNotFoundException("Contact info not found");

            var email = new Email(request.Dto.Email);
            var phone = new PhoneNumber(request.Dto.Phone);

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

            contactInfo.Update(email, phone, address, socialMedia, request.Dto.OfficeHours);

            _repository.Update(contactInfo, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);


        }
    }
}
