using RacksStand.Api.Core.Domain.Exceptions;

namespace RacksStand.Api.Core.Domain.ValueObjects;

public record PlanPrice
{
    public decimal Value { get; }

    private PlanPrice(decimal amount) => Value = amount;

    public static PlanPrice Of(decimal amount)
    {
        if (amount < 0)
        {
            throw new InvalidPriceException("PlanPrice cannot be negative.");
        }

        return new PlanPrice(amount);
    }
}
