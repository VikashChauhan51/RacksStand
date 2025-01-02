using RacksStand.Domain.Core;

namespace RacksStand.Domain.ValueObjects;
public record YAxis : IValueObject<float>
{
    private readonly float _value;
    public float Value => _value;
    private YAxis(float value) => _value = value;
    public static YAxis Of(float value)
    {
        if (value <= 0) throw new ArgumentException("YAxis must be greater than zero.", nameof(value));

        return new YAxis(value);
    }
}
