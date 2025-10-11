using RacksStand.Domain.Core;

namespace RacksStand.Domain.ValueObjects;

public record Length : IValueObject<decimal>
{
    private readonly decimal _value;
    public decimal Value => _value;
    public Length(decimal value)
    {
        if (value <= 0) throw new ArgumentException("Length must be greater than zero.", nameof(value));
        _value = value;
    }

    public static Length Of(decimal value) => new(value);
    public static implicit operator Length(decimal value) => Of(value);
    public static explicit operator decimal(Length length) => length.Value;
}
