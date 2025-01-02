using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record PurchasePrice : IValueObject<decimal>
{
    private readonly decimal _value;
    public decimal Value => _value;

    private PurchasePrice(decimal amount) => _value = amount;

    public static PurchasePrice Of(decimal amount)
    {
        if (amount < 0)
        {
            throw new InvalidPriceException("PurchasePrice cannot be negative.");
        }

        return new PurchasePrice(amount);
    }
}
