using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record UserInviteId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private UserInviteId(string value) => _value = value;
    public static UserInviteId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("UserInviteId cannot be null or whitespace.");
        }

        return new UserInviteId(value);
    }
}
