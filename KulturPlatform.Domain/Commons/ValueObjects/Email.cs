namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record Email
    {
        public string Value { get; init; }

        // Private parameterless constructor for EF Core
        private Email() { Value = string.Empty; }

        public Email(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty.", nameof(email));

            email = email.Trim();

            if (!IsValidEmail(email))
                throw new ArgumentException("Invalid email format.", nameof(email));

            Value = email;
        }

        public static Email Create(string email)
        {
            return new Email(email);
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public override string ToString() => Value;
    }
}
