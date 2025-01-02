using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record PlanFeatureId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private PlanFeatureId(string value) => _value = value;
    public static PlanFeatureId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("PlanFeatureId cannot be null or whitespace.");
        }

        return new PlanFeatureId(value);
    }
}
