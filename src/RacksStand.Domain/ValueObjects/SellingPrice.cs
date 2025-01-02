using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record SellingPrice : IValueObject<decimal>
{
    private readonly decimal _value;
    public decimal Value => _value;

    private SellingPrice(decimal amount) => _value = amount;

    public static SellingPrice Of(decimal amount)
    {
        if (amount < 0)
        {
            throw new InvalidPriceException("SellingPrice cannot be negative.");
        }

        return new SellingPrice(amount);
    }
}
