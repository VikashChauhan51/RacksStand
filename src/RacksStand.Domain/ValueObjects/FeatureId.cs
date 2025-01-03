using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record FeatureId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public FeatureId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("FeatureId cannot be null or whitespace.");
        }
        _value = value;
    }
    public static FeatureId Of(string value) => new(value);
    public static implicit operator FeatureId(string value) => Of(value);
    public static explicit operator string(FeatureId featureId) => featureId.Value;
}
