using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record SupplierId
{
    public string Value { get; }
    private SupplierId(string value) => Value = value;
    public static SupplierId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("SupplierId cannot be null or whitespace.");
        }

        return new SupplierId(value);
    }
}
