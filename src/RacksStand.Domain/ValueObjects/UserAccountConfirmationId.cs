using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record UserAccountConfirmationId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public UserAccountConfirmationId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("AccountConfirmationId cannot be null or whitespace.");
        }

        _value = value;
    }

    public static UserAccountConfirmationId Of(string value) => new(value);
    public static implicit operator UserAccountConfirmationId(string value) => Of(value);
    public static explicit operator string(UserAccountConfirmationId  confirmationId) => confirmationId.Value;
}

