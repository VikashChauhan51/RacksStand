using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record PlanPrice : IValueObject<decimal>
{
    private readonly decimal _value;
    public decimal Value => _value;

    private PlanPrice(decimal amount) => _value = amount;

    public static PlanPrice Of(decimal amount)
    {
        if (amount < 0)
        {
            throw new InvalidPriceException("PlanPrice cannot be negative.");
        }

        return new PlanPrice(amount);
    }
}
