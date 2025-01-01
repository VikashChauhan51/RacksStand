using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record PurchasePrice
{
    public decimal Value { get; }

    private PurchasePrice(decimal amount) => Value = amount;

    public static PurchasePrice Of(decimal amount)
    {
        if (amount < 0)
        {
            throw new InvalidPriceException("PurchasePrice cannot be negative.");
        }

        return new PurchasePrice(amount);
    }
}
