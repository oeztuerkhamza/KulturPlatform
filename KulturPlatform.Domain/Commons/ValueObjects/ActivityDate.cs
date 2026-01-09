using System.Globalization;

namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public sealed record ActivityDate
    {
        public DateTime DateIso { get; init; }

        // EF Core için
        private ActivityDate()
        {
            DateIso = DateTime.MinValue;
        }

        private ActivityDate(DateTime dateIso)
        {
            if (dateIso == DateTime.MinValue)
                throw new ArgumentException("Date cannot be empty.", nameof(dateIso));

            DateIso = DateTime.SpecifyKind(dateIso, DateTimeKind.Utc);
        }

        public static ActivityDate Create(DateTime dateIso)
            => new(dateIso);

        public static ActivityDate FromString(string date)
        {
            if (!DateTime.TryParse(date, null, DateTimeStyles.AssumeUniversal, out var parsedDate))
                throw new ArgumentException("Invalid date format", nameof(date));
            return Create(parsedDate);
        }


        // Derived / computed values (domain-safe)
        public string ToTrString()
            => DateIso.ToString("dd MMMM yyyy", new CultureInfo("tr-TR"));

        public string ToDeString()
            => DateIso.ToString("dd. MMMM yyyy", new CultureInfo("de-DE"));

        public override string ToString()
            => DateIso.ToString("yyyy-MM-dd");
    }
}
