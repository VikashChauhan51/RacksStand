using RacksStand.Domain.Core;

namespace RacksStand.Domain.ValueObjects;
public record Quantity : IValueObject<int>
{
    private readonly int _value;
    public int Value => _value;
    private Quantity(int value) => _value = value;
    public static Quantity Of(int value)
    {
        if (value <= 0) throw new ArgumentException("Quantity must be greater than zero.", nameof(value));

        return new Quantity(value);
    }
}

