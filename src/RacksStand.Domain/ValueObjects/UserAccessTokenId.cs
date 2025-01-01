using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record UserAccessTokenId
{
    public string Value { get; }
    private UserAccessTokenId(string value) => Value = value;
    public static UserAccessTokenId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("UserAccessTokenId cannot be null or whitespace.");
        }
        return new UserAccessTokenId(value);
    }
}
