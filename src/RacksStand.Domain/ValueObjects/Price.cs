using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record Price
{
    public decimal Amount { get; }
    public string Currency { get; } // Or a Currency Value Object for more complex scenarios

    private Price(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Price Of(decimal amount, string currency)
    {
        if (amount < 0)
        {
            throw new InvalidPriceException("Price cannot be negative.");
        }
        if (string.IsNullOrWhiteSpace(currency))
        {
            throw new InvalidPriceException("Currency cannot be null or empty.");
        }
        return new Price(amount, currency);
    }
}
