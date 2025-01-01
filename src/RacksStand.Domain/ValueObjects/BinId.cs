using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record BinId
{
    public string Value { get; }
    private BinId(string value) => Value = value;
    public static BinId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("BinId cannot be null or whitespace.");
        }

        return new BinId(value);
    }
}
