using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record PlanFeatureId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public PlanFeatureId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("PlanFeatureId cannot be null or whitespace.");
        }
        _value = value;
    }
    public static PlanFeatureId Of(string value) => new(value);
    public static implicit operator PlanFeatureId(string value) => Of(value);
    public static explicit operator string(PlanFeatureId planFeatureId) => planFeatureId.Value;
}
