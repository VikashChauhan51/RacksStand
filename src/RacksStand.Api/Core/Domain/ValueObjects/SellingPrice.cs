using RacksStand.Api.Core.Domain.Exceptions;

namespace RacksStand.Api.Core.Domain.ValueObjects;

public record SellingPrice
{
    public decimal Value { get; }

    private SellingPrice(decimal amount) => Value = amount;

    public static SellingPrice Of(decimal amount)
    {
        if (amount < 0)
        {
            throw new InvalidPriceException("SellingPrice cannot be negative.");
        }

        return new SellingPrice(amount);
    }
}
