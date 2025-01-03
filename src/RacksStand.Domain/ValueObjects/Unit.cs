using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record Unit : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public Unit(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Unit cannot be null or whitespace.");
        }
        _value = value;
    }

    public static Unit Of(string value) => new(value);
    public static implicit operator Unit(string value) => Of(value);
    public static explicit operator string(Unit unit) => unit.Value;
}
