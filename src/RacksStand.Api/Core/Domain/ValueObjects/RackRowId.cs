using RacksStand.Api.Core.Domain.Exceptions;

namespace RacksStand.Api.Core.Domain.ValueObjects;
public record RackRowId
{
    public string Value { get; }
    private RackRowId(string value) => Value = value;
    public static RackRowId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("RackRowId cannot be null or whitespace.");
        }

        return new RackRowId(value);
    }
}
