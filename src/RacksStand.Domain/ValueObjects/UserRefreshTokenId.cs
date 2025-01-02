using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record UserRefreshTokenId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private UserRefreshTokenId(string value) => _value = value;
    public static UserRefreshTokenId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("UserRefreshTokenId cannot be null or whitespace.");
        }
        return new UserRefreshTokenId(value);
    }
}
