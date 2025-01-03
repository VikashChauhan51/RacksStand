using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record UserAccessTokenId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public UserAccessTokenId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("UserAccessTokenId cannot be null or whitespace.");
        }
        _value = value;
    }
    public static UserAccessTokenId Of(string value) => new(value);
    public static implicit operator UserAccessTokenId(string value) => Of(value);
    public static explicit operator string(UserAccessTokenId accessTokenId) => accessTokenId.Value;
}
