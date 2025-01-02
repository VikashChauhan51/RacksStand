using RacksStand.Domain.Core;

namespace RacksStand.Domain.ValueObjects;

public record Height : IValueObject<decimal>
{
    private readonly decimal _value;
    public decimal Value => _value;
    private Height(decimal value) => _value = value;
    public static Height Of(decimal value)
    {
        if (value <= 0) throw new ArgumentException("Height must be greater than zero.", nameof(value));

        return new Height(value);
    }
}
