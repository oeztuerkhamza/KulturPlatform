namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record LocalizedText
    {
        public string Tr { get; init; }
        public string De { get; init; }

        // Private parameterless constructor for EF Core
        private LocalizedText() 
        { 
            Tr = string.Empty;
            De = string.Empty;
        }

        public LocalizedText(string tr, string de)
        {
            if (string.IsNullOrWhiteSpace(tr))
                throw new ArgumentException("Turkish text cannot be empty.");
            if (string.IsNullOrWhiteSpace(de))
                throw new ArgumentException("German text cannot be empty.");

            Tr = tr.Trim();
            De = de.Trim();
        }

        public override string ToString() => $"{Tr} | {De}";
    }

}
