using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record PocketId
{
    public string Value { get; }
    private PocketId(string value) => Value = value;
    public static PocketId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("PocketId cannot be null or whitespace.");
        }

        return new PocketId(value);
    }
}
