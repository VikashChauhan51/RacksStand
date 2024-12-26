using RacksStand.Persistence.Exceptions;

namespace RacksStand.Persistence.ValueObjects;
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
