using RacksStand.Domain.Core;

namespace RacksStand.Domain.ValueObjects;

public record Height : IValueObject<decimal>
{
    private readonly decimal _value;
    public decimal Value => _value;
    public Height(decimal value)
    {
        if (value <= 0) throw new ArgumentException("Height must be greater than zero.", nameof(value));
        _value = value;
    }
    public static Height Of(decimal value) => new(value);
    public static implicit operator Height(decimal value) => Of(value);
    public static explicit operator decimal(Height height) => height.Value;
}
