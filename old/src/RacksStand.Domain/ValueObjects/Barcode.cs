using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record Barcode : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public Barcode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Barcode cannot be null or whitespace.");
        }
        _value = value;
    }
    public static Barcode Of(string value) => new(value);

    public static implicit operator Barcode(string value) => Of(value);
    public static explicit operator string(Barcode barcode) => barcode.Value;
}
