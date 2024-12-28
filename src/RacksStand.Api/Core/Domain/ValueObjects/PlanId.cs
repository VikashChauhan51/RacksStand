using RacksStand.Api.Core.Domain.Exceptions;

namespace RacksStand.Api.Core.Domain.ValueObjects;

public record PlanId
{
    public string Value { get; }
    private PlanId(string value) => Value = value;
    public static PlanId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("PlanId cannot be null or whitespace.");
        }

        return new PlanId(value);
    }
}
