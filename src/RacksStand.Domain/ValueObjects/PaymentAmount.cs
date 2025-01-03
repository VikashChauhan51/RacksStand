using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record PaymentAmount : IValueObject<decimal>
{
    private readonly decimal _value;
    public decimal Value => _value;

    public PaymentAmount(decimal amount)
    {
        if (amount < 0)
        {
            throw new InvalidPriceException("PaymentAmount cannot be negative.");
        }
        _value = amount;
    }

    public static PaymentAmount Of(decimal amount) => new(amount);
    public static implicit operator PaymentAmount(decimal value) => Of(value);
    public static explicit operator decimal(PaymentAmount payment) => payment.Value;
}
