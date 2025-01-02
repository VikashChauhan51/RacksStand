using RacksStand.Domain.Core;

namespace RacksStand.Domain.ValueObjects;

public record Weight : IValueObject<decimal>
{
    private readonly decimal _value;
    public decimal Value => _value;
    private Weight(decimal value) => _value = value;
    public static Weight Of(decimal value)
    {
        if (value <= 0) throw new ArgumentException("Weight must be greater than zero.", nameof(value));

        return new Weight(value);
    }
}
