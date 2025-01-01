using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record PlanFeatureId
{
    public string Value { get; }
    private PlanFeatureId(string value) => Value = value;
    public static PlanFeatureId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("PlanFeatureId cannot be null or whitespace.");
        }

        return new PlanFeatureId(value);
    }
}
