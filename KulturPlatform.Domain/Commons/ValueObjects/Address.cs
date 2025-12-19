namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record Address
    {
        public string Street { get; init; }
        public string HouseNo { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string Country { get; init; }
        public string ZipCode { get; init; }

        // Private parameterless constructor for EF Core
        private Address()
        {
            Street = string.Empty;
            HouseNo = string.Empty;
            ZipCode = string.Empty;
            City = string.Empty;
            State = string.Empty;
            Country = string.Empty;

        }

        public Address(string street, string houseNo, string zipCode, string city, string state, string country)
        {
            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentException("Street cannot be empty.", nameof(street));
            if (string.IsNullOrWhiteSpace(houseNo))
                throw new ArgumentException("House number cannot be empty.", nameof(houseNo));
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City cannot be empty.", nameof(city));
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country cannot be empty.", nameof(country));

            Street = street.Trim();
            HouseNo = houseNo.Trim();
            ZipCode = zipCode?.Trim() ?? string.Empty;
            City = city.Trim();
            State = state?.Trim() ?? string.Empty;
            Country = country.Trim();

        }

        public override string ToString()
        {
            return $"{Street}, {HouseNo},{ZipCode}, {City}, {State}, {Country} ";
        }

    }
}
