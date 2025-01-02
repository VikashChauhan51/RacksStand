using RacksStand.Domain.Core;

namespace RacksStand.Domain.ValueObjects;

public record XAxis : IValueObject<float>
{
    private readonly float _value;
    public float Value => _value;
    private XAxis(float value) => _value = value;
    public static XAxis Of(float value)
    {
        if (value <= 0) throw new ArgumentException("XAxis must be greater than zero.", nameof(value));

        return new XAxis(value);
    }
}
