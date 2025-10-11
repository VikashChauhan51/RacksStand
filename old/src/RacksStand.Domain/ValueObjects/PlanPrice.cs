using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record PlanPrice : IValueObject<decimal>
{
    private readonly decimal _value;
    public decimal Value => _value;

    public PlanPrice(decimal amount)
    {
        if (amount < 0)
        {
            throw new InvalidPriceException("PlanPrice cannot be negative.");
        }
        _value = amount;
    }

    public static PlanPrice Of(decimal amount) => new(amount);
    public static implicit operator PlanPrice(decimal value) => Of(value);
    public static explicit operator decimal(PlanPrice price) => price.Value;
}
