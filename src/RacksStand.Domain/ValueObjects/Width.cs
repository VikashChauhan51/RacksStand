using RacksStand.Domain.Core;

namespace RacksStand.Domain.ValueObjects;

public record Width : IValueObject<decimal>
{
    private readonly decimal _value;
    public decimal Value => _value;
    public Width(decimal value)
    {
        if (value <= 0) throw new ArgumentException("Width must be greater than zero.", nameof(value));
        _value = value;
    }
    public static Width Of(decimal value) => new(value);
    public static implicit operator Width(decimal value) => Of(value);
    public static explicit operator decimal(Width width) => width.Value;
}
