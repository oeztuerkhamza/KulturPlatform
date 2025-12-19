using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Aggregates
{
    public class VolunteerSubmission : AggregateRoot, IAggregateRoot
    {
        public Name FullName { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public SubmissionMessage Message { get; private set; }
        public DateTime SubmittedAt { get; private set; }

        private VolunteerSubmission(Guid id) : base(id) { }

        private VolunteerSubmission(
            Guid id,
            Name fullName,
            Email email,
            PhoneNumber phoneNumber,
            SubmissionMessage message)
            : base(id)
        {
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            Message = message;
            SubmittedAt = DateTime.UtcNow;
        }
        public static VolunteerSubmission CreateNew(
            Name fullName,
            Email email,
            PhoneNumber phoneNumber,
            SubmissionMessage message)
        {
            return new VolunteerSubmission(Guid.NewGuid(), fullName, email, phoneNumber, message);
        }

    }
}
