using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record SupplierId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private SupplierId(string value) => _value = value;
    public static SupplierId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("SupplierId cannot be null or whitespace.");
        }

        return new SupplierId(value);
    }
}
