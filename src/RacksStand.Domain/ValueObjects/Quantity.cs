namespace RacksStand.Domain.ValueObjects;
public record Quantity
{
    public int Value { get; }
    private Quantity(int value) => Value = value;
    public static Quantity Of(int value)
    {
        if (value <= 0) throw new ArgumentException("Quantity must be greater than zero.", nameof(value));

        return new Quantity(value);
    }
}

