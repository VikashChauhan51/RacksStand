namespace RacksStand.Domain.ValueObjects;

public record Length
{
    public decimal Value { get; }
    private Length(decimal value) => Value = value;
    public static Length Of(decimal value)
    {
        if (value <= 0) throw new ArgumentException("Length must be greater than zero.", nameof(value));

        return new Length(value);
    }
}
