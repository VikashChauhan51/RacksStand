using RacksStand.Api.Core.Domain.Exceptions;

namespace RacksStand.Api.Core.Domain.ValueObjects;

public record ActivityId
{
    public string Value { get; }
    private ActivityId(string value) => Value = value;
    public static ActivityId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("ActivityId cannot be null or whitespace.");
        }

        return new ActivityId(value);
    }
}
