using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record PurchaseOrderId
{
    public string Value { get; }
    private PurchaseOrderId(string value) => Value = value;
    public static PurchaseOrderId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("PurchaseOrderId cannot be null or whitespace.");
        }

        return new PurchaseOrderId(value);
    }
}
