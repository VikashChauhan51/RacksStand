using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record PocketItemId
{
    public string Value { get; }
    private PocketItemId(string value) => Value = value;
    public static PocketItemId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("PocketItemId cannot be null or whitespace.");
        }

        return new PocketItemId(value);
    }
}
