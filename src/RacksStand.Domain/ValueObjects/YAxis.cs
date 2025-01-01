namespace RacksStand.Domain.ValueObjects;
public record YAxis
{
    public float Value { get; }
    private YAxis(float value) => Value = value;
    public static YAxis Of(float value)
    {
        if (value <= 0) throw new ArgumentException("YAxis must be greater than zero.", nameof(value));

        return new YAxis(value);
    }
}
