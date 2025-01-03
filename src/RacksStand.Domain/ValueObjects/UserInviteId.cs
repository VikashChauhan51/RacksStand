using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record UserInviteId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public UserInviteId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("UserInviteId cannot be null or whitespace.");
        }
        _value = value;
    }
    public static UserInviteId Of(string value) => new(value);
    public static implicit operator UserInviteId(string value) => Of(value);
    public static explicit operator string(UserInviteId userInviteId) => userInviteId.Value;
}
