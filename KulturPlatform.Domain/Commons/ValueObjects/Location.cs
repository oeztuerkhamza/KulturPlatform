namespace KulturPlatform.Domain.Commons.ValueObjects;

public sealed record Location
{
    public string Value { get; }

    private Location(string value)
    {
        Value = value;
    }

    public static Location Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Location cannot be empty");

        return new Location(value.Trim());
    }

    public override string ToString() => Value;
}