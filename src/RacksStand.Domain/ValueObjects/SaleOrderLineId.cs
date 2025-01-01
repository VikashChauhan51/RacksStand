using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record SaleOrderLineId
{
    public string Value { get; }
    private SaleOrderLineId(string value) => Value = value;
    public static SaleOrderLineId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("SaleOrderId cannot be null or whitespace.");
        }

        return new SaleOrderLineId(value);
    }
}
