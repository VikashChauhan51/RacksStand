using RacksStand.Domain.Core;

namespace RacksStand.Domain.ValueObjects;

public record Length : IValueObject<decimal>
{
    private readonly decimal _value;
    public decimal Value => _value;
    private Length(decimal value) => _value = value;
    public static Length Of(decimal value)
    {
        if (value <= 0) throw new ArgumentException("Length must be greater than zero.", nameof(value));

        return new Length(value);
    }
}
