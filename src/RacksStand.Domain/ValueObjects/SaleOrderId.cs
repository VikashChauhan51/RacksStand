using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record SaleOrderId
{
    public string Value { get; }
    private SaleOrderId(string value) => Value = value;
    public static SaleOrderId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("SaleOrderId cannot be null or whitespace.");
        }

        return new SaleOrderId(value);
    }
}
