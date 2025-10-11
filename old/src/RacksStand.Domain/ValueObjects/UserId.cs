using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record UserId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public UserId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("UserId cannot be null or whitespace.");
        }
        _value = value;
    }
    public static UserId Of(string value) => new(value);
    public static implicit operator UserId(string value) => Of(value);
    public static explicit operator string(UserId userId) => userId.Value;
}
