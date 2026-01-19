using KulturPlatform.Domain.Commons.ValueObjects;
using KulturPlatform.Domain.Interfaces;

namespace KulturPlatform.Domain.Commons.Aggregates
{
    public class ContactMessage : AggregateRoot, IAggregateRoot
    {
        public Anrede? Anrede { get; private set; }
        public Name SenderName { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber? Phone { get; private set; }
        public Subject Subject { get; private set; }
        public MessageText Message { get; private set; }
        public DateTime SubmittedAt { get; private set; }
        public bool IsRead { get; private set; }
        public DateTime? ReadAt { get; private set; }

        private ContactMessage(Guid id) : base(id) { }

        private ContactMessage(
            Guid id,
            Anrede? anrede,
            Name senderName,
            Email email,
            PhoneNumber? phone,
            Subject subject,
            MessageText message)
            : base(id)
        {
            Anrede = anrede;
            SenderName = senderName;
            Email = email;
            Phone = phone;
            Subject = subject;
            Message = message;
            SubmittedAt = DateTime.UtcNow;
            IsRead = false;
        }

        public static ContactMessage CreateNew(
            Anrede? anrede,
            Name senderName,
            Email email,
            PhoneNumber? phone,
            Subject subject,
            MessageText message)
        {
            return new ContactMessage(Guid.NewGuid(), anrede, senderName, email, phone, subject, message);
        }

        public void MarkAsRead()
        {
            if (!IsRead)
            {
                IsRead = true;
                ReadAt = DateTime.UtcNow;
            }
        }
    }
}
