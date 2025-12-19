using KulturPlatform.Domain.Commons.Entities;
using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Aggregates
{
    public class ContactInfo : AuditableEntity, IAggregateRoot
    {
        public Email Email { get; private set; }
        public PhoneNumber Phone { get; private set; }
        public Address Address { get; private set; }
        public SocialMediaLinks SocialMedia { get; private set; }
        public string OfficeHours { get; private set; }

        private ContactInfo(Guid id) : base(id)
        {
        }

        public static ContactInfo Create(Email email, PhoneNumber phoneNumber, Address address,
            SocialMediaLinks socialMediaLinks, string officeHours)
        {
            return new ContactInfo(Guid.NewGuid())
            {
                Email = email,
                Phone = phoneNumber,
                Address = address,
                SocialMedia = socialMediaLinks,
                OfficeHours = officeHours,
                CreatedAt = DateTime.UtcNow,
            };
        }

        public void Update(Email email, PhoneNumber phone, Address address, SocialMediaLinks socialMedia,
            string officeHours)
        {
            Email = email;
            Phone = phone;
            Address = address;
            SocialMedia = socialMedia;
            OfficeHours = officeHours;
            SetUpdatedAt();
        }
    }
}