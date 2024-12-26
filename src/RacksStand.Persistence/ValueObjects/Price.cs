using RacksStand.Persistence.Exceptions;

namespace RacksStand.Persistence.ValueObjects;
public record Price
{
    public decimal Value { get; }

    private Price(decimal amount) => Value = amount;

    public static Price Of(decimal amount)
    {
        if (amount < 0)
        {
            throw new InvalidPriceException("Price cannot be negative.");
        }

        return new Price(amount);
    }
}
