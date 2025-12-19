using System.Text.Json.Serialization;

namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record ActivityDate
    {
        public string DateTr { get; init; }
        public string DateDe { get; init; }
        public DateTime DateISO { get; init; }

        // Private parameterless constructor for EF Core
        private ActivityDate()
        {
            DateTr = string.Empty;
            DateDe = string.Empty;
            DateISO = DateTime.MinValue;
        }
        [JsonConstructor]
        public ActivityDate(string dateTr, string dateDe, DateTime dateISO)
        {
            if (string.IsNullOrWhiteSpace(dateTr))
                throw new ArgumentException("DateTr cannot be empty.", nameof(dateTr));

            if (string.IsNullOrWhiteSpace(dateDe))
                throw new ArgumentException("DateDe cannot be empty.", nameof(dateDe));

            DateTr = dateTr.Trim();
            DateDe = dateDe.Trim();
            DateISO = dateISO;
        }

        public override string ToString() => $"{DateTr} / {DateDe} / {DateISO:yyyy-MM-dd}";
    }
}
