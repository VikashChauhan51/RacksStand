using RacksStand.Api.Core.Domain.Exceptions;

namespace RacksStand.Api.Features.Shops.Domain.ValueObjects;
public record UserShopId
{
    public string Value { get; }
    private UserShopId(string value) => Value = value;
    public static UserShopId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("UserShopId cannot be null or whitespace.");
        }

        return new UserShopId(value);
    }
}
