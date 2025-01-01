using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record Barcode
{
    public string Value { get; }
    private Barcode(string value) => Value = value;
    public static Barcode Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Barcode cannot be null or whitespace.");
        }

        return new Barcode(value);
    }
}
