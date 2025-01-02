using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record UserAccessTokenId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private UserAccessTokenId(string value) => _value = value;
    public static UserAccessTokenId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("UserAccessTokenId cannot be null or whitespace.");
        }
        return new UserAccessTokenId(value);
    }
}
