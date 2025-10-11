using RacksStand.Domain.Core;

namespace RacksStand.Domain.ValueObjects;

public record Weight : IValueObject<decimal>
{
    private readonly decimal _value;
    public decimal Value => _value;
    public Weight(decimal value)
    {
        if (value <= 0) throw new ArgumentException("Weight must be greater than zero.", nameof(value));
        _value = value;
    }
    public static Weight Of(decimal value) => new(value);
    public static implicit operator Weight(decimal value) => Of(value);
    public static explicit operator decimal(Weight weight) => weight.Value;
}
