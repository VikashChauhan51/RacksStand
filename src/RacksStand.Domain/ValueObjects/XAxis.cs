namespace RacksStand.Domain.ValueObjects;

public record XAxis
{
    public float Value { get; }
    private XAxis(float value) => Value = value;
    public static XAxis Of(float value)
    {
        if (value <= 0) throw new ArgumentException("XAxis must be greater than zero.", nameof(value));

        return new XAxis(value);
    }
}
