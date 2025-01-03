using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record SupplierId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public SupplierId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("SupplierId cannot be null or whitespace.");
        }
        _value = value;
    }
    public static SupplierId Of(string value) => new(value);
    public static implicit operator SupplierId(string value) => Of(value);
    public static explicit operator string(SupplierId supplierId) => supplierId.Value;
}
