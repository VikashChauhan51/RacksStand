using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record UserRefreshTokenId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public UserRefreshTokenId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("UserRefreshTokenId cannot be null or whitespace.");
        }
        _value = value;
    }
    public static UserRefreshTokenId Of(string value) => new(value);
    public static implicit operator UserRefreshTokenId(string value) => Of(value);
    public static explicit operator string(UserRefreshTokenId refreshTokenId) => refreshTokenId.Value;
}
