using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record PaymentAmount
{
    public decimal Value { get; }

    private PaymentAmount(decimal amount) => Value = amount;

    public static PaymentAmount Of(decimal amount)
    {
        if (amount < 0)
        {
            throw new InvalidPriceException("PaymentAmount cannot be negative.");
        }

        return new PaymentAmount(amount);
    }
}
