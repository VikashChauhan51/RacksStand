namespace RacksStand.Domain.ValueObjects;

public record Weight
{
    public decimal Value { get; }
    private Weight(decimal value) => Value = value;
    public static Weight Of(decimal value)
    {
        if (value <= 0) throw new ArgumentException("Weight must be greater than zero.", nameof(value));

        return new Weight(value);
    }
}
