using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record ShopId
{
    public string Value { get; }
    private ShopId(string value) => Value = value;
    public static ShopId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("ShopId cannot be null or whitespace.");
        }

        return new ShopId(value);
    }
}
