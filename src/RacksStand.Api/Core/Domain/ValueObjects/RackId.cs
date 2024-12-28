using RacksStand.Api.Core.Domain.Exceptions;

namespace RacksStand.Api.Core.Domain.ValueObjects;
public record RackId
{
    public string Value { get; }
    private RackId(string value) => Value = value;
    public static RackId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("RackId cannot be null or whitespace.");
        }
        return new RackId(value);
    }
}
