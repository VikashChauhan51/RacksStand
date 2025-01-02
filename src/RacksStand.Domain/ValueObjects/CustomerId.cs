using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record CustomerId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private CustomerId(string value) => _value = value;
    public static CustomerId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("CustomerId cannot be null or whitespace.");
        }

        return new CustomerId(value);
    }
}
