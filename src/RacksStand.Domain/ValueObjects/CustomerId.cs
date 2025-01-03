using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record CustomerId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public CustomerId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("CustomerId cannot be null or whitespace.");
        }

        _value = value;
    }
    public static CustomerId Of(string value) => new(value);
    public static implicit operator CustomerId(string value) => Of(value);
    public static explicit operator string(CustomerId customerId) => customerId.Value;
}
