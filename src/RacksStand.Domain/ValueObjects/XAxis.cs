using RacksStand.Domain.Core;

namespace RacksStand.Domain.ValueObjects;

public record XAxis : IValueObject<float>
{
    private readonly float _value;
    public float Value => _value;
    public XAxis(float value)
    {
        if (value <= 0) throw new ArgumentException("XAxis must be greater than zero.", nameof(value));
        _value = value;
    }
    public static XAxis Of(float value) => new(value);
    public static implicit operator XAxis(float value) => Of(value);
    public static explicit operator float(XAxis xAxis) => xAxis.Value;
}
