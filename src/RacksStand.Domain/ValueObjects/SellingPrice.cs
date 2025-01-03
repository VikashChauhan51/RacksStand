using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record SellingPrice : IValueObject<decimal>
{
    private readonly decimal _value;
    public decimal Value => _value;

    public SellingPrice(decimal amount)
    {
        if (amount < 0)
        {
            throw new InvalidPriceException("SellingPrice cannot be negative.");
        }
        _value = amount;
    }

    public static SellingPrice Of(decimal amount) => new(amount);
    public static implicit operator SellingPrice(decimal value) => Of(value);
    public static explicit operator decimal(SellingPrice price) => price.Value;
}
