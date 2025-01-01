using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record UserRefreshTokenId
{
    public string Value { get; }
    private UserRefreshTokenId(string value) => Value = value;
    public static UserRefreshTokenId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("UserRefreshTokenId cannot be null or whitespace.");
        }
        return new UserRefreshTokenId(value);
    }
}
