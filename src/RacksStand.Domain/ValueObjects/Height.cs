namespace RacksStand.Domain.ValueObjects;


public record Height
{
    public decimal Value { get; }
    private Height(decimal value) => Value = value;
    public static Height Of(decimal value)
    {
        if (value <= 0) throw new ArgumentException("Height must be greater than zero.", nameof(value));

        return new Height(value);
    }
}
