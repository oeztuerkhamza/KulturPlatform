using KulturPlatform.Domain.Commons.ValueObjects;

namespace KulturPlatform.Domain.Commons.Aggregates
{
    public class NewsletterSubscriber
    {
        public Guid Id { get; private set; }
        public Email Email { get; private set; }
        public string? FullName { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsVerified { get; private set; }
        public DateTime SubscribedAt { get; private set; }
        public DateTime? VerifiedAt { get; private set; }
        public DateTime? UnsubscribedAt { get; private set; }
        public string UnsubscribeToken { get; private set; }
        public string? VerificationToken { get; private set; }
        public SubscriptionSource Source { get; private set; }

        // EF Core constructor
        private NewsletterSubscriber() { }

        public static NewsletterSubscriber Create(
            Email email,
            string? fullName = null,
            SubscriptionSource source = SubscriptionSource.Website)
        {
            return new NewsletterSubscriber
            {
                Id = Guid.NewGuid(),
                Email = email,
                FullName = fullName,
                IsActive = true,
                IsVerified = false, // Double opt-in için false ba?lar
                SubscribedAt = DateTime.UtcNow,
                UnsubscribeToken = GenerateToken(),
                VerificationToken = GenerateToken(),
                Source = source
            };
        }

        public void Verify()
        {
            if (IsVerified)
                throw new InvalidOperationException("Already verified");

            IsVerified = true;
            VerifiedAt = DateTime.UtcNow;
            VerificationToken = null; // Token'? temizle
        }

        public void Unsubscribe()
        {
            if (!IsActive)
                throw new InvalidOperationException("Already unsubscribed");

            IsActive = false;
            UnsubscribedAt = DateTime.UtcNow;
        }

        public void Reactivate()
        {
            if (IsActive)
                throw new InvalidOperationException("Already active");

            IsActive = true;
            UnsubscribedAt = null;
        }

        private static string GenerateToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                .Replace("/", "_")
                .Replace("+", "-")
                .TrimEnd('=');
        }
    }

    public enum SubscriptionSource
    {
        Website = 1,
        Manual = 2,
        Import = 3,
        Api = 4
    }
}
