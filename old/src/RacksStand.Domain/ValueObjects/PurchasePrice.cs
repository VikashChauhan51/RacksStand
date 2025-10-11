using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record PurchasePrice : IValueObject<decimal>
{
    private readonly decimal _value;
    public decimal Value => _value;

    public PurchasePrice(decimal amount)
    {
        if (amount < 0)
        {
            throw new InvalidPriceException("PurchasePrice cannot be negative.");
        }
        _value = amount;
    }

    public static PurchasePrice Of(decimal amount) => new(amount);
    public static implicit operator PurchasePrice(decimal value) => Of(value);
    public static explicit operator decimal(PurchasePrice price) => price.Value;
}
