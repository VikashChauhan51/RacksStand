using RacksStand.Domain.Core;

namespace RacksStand.Domain.ValueObjects;

public record Width : IValueObject<decimal>
{
    private readonly decimal _value;
    public decimal Value => _value;
    private Width(decimal value) => _value = value;
    public static Width Of(decimal value)
    {
        if (value <= 0) throw new ArgumentException("Width must be greater than zero.", nameof(value));

        return new Width(value);
    }
}
