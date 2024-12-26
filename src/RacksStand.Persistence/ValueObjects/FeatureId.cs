using RacksStand.Persistence.Exceptions;

namespace RacksStand.Persistence.ValueObjects;
public record FeatureId
{
    public string Value { get; }
    private FeatureId(string value) => Value = value;
    public static FeatureId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("FeatureId cannot be null or whitespace.");
        }

        return new FeatureId(value);
    }
}
