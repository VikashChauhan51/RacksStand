using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record PaymentAmount : IValueObject<decimal>
{
    private readonly decimal _value;
    public decimal Value => _value;

    private PaymentAmount(decimal amount) => _value = amount;

    public static PaymentAmount Of(decimal amount)
    {
        if (amount < 0)
        {
            throw new InvalidPriceException("PaymentAmount cannot be negative.");
        }

        return new PaymentAmount(amount);
    }
}
