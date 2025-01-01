using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record UserInviteId
{
    public string Value { get; }
    private UserInviteId(string value) => Value = value;
    public static UserInviteId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("UserInviteId cannot be null or whitespace.");
        }

        return new UserInviteId(value);
    }
}
