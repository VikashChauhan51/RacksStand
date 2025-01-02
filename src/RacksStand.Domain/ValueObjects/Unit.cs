using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record Unit : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private Unit(string value) => _value = value;

    public static Unit Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Unit cannot be null or whitespace.");
        }

        return new Unit(value);
    }
}
