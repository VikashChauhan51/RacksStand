namespace RacksStand.Domain.ValueObjects;

public record Width
{
    public decimal Value { get; }
    private Width(decimal value) => Value = value;
    public static Width Of(decimal value)
    {
        if (value <= 0) throw new ArgumentException("Width must be greater than zero.", nameof(value));

        return new Width(value);
    }
}
