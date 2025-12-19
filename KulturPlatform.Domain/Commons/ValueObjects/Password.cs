using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record Password
    {
        public string Value { get; init; }

        // Private parameterless constructor for EF Core
        private Password() { Value = string.Empty; }
        [JsonConstructor]
        public Password(string hashedValue)
        {
            Value = hashedValue;
        }

        public static Password Create(string plainPassword)
        {
            ValidatePassword(plainPassword);
            var hashed = HashPassword(plainPassword);
            return new Password(hashed);
        }

        public static Password FromHash(string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(hashedPassword))
                throw new ArgumentException("Hashed password cannot be empty.", nameof(hashedPassword));

            return new Password(hashedPassword);
        }

        public bool Verify(string plainPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainPassword, Value);
        }
        private static void ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty.", nameof(password));
            if (password.Length < 8)
                throw new ArgumentException("Password must be at least 8 characters long.", nameof(password));
            if (!Regex.IsMatch(password, @"[A-Z]"))
                throw new ArgumentException("Password must contain at least one uppercase letter.", nameof(password));
            if (!Regex.IsMatch(password, @"[a-z]"))
                throw new ArgumentException("Password must contain at least one lowercase letter.", nameof(password));
            if (!Regex.IsMatch(password, @"[0-9]"))
                throw new ArgumentException("Password must contain at least one digit.", nameof(password));
            if (!Regex.IsMatch(password, @"[\W_]"))
                throw new ArgumentException("Password must contain at least one special character.", nameof(password));
        }
        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public override string ToString() => Value;
    }
}
