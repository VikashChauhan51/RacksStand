using RacksStand.Domain.Core;

namespace RacksStand.Domain.ValueObjects;
public record YAxis : IValueObject<float>
{
    private readonly float _value;
    public float Value => _value;
    public YAxis(float value)
    {
        if (value <= 0) throw new ArgumentException("YAxis must be greater than zero.", nameof(value));
        _value = value;
    }
    public static YAxis Of(float value) => new(value);
    public static implicit operator YAxis(float value) => Of(value);
    public static explicit operator float(YAxis yAxis) => yAxis.Value;
}
