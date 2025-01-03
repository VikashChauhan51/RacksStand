using RacksStand.Domain.Core;

namespace RacksStand.Domain.ValueObjects;
public record Quantity : IValueObject<int>
{
    private readonly int _value;
    public int Value => _value;
    public Quantity(int value)
    {
        if (value <= 0) throw new ArgumentException("Quantity must be greater than zero.", nameof(value));
        _value = value;
    }
    public static Quantity Of(int value) => new(value);
    public static implicit operator Quantity(int value) => Of(value);
    public static explicit operator int(Quantity quantity) => quantity.Value;
}

