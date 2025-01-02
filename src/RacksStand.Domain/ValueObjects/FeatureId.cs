using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record FeatureId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private FeatureId(string value) => _value = value;
    public static FeatureId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("FeatureId cannot be null or whitespace.");
        }

        return new FeatureId(value);
    }
}
