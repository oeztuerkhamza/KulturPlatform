namespace KulturPlatform.Domain.Commons.ValueObjects
{
    public record DisplayOrder
    {
        public int Value { get; init; }

        // Private parameterless constructor for EF Core
        private DisplayOrder() { Value = 0; }

        public DisplayOrder(int value)
        {
            if (value < 0)
                throw new ArgumentException("Display order cannot be negative.");

            Value = value;
        }

        public override string ToString() => Value.ToString();
    }

}
