using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
 
public record PurchaseOrderLineId
{
    public string Value { get; }
    private PurchaseOrderLineId(string value) => Value = value;
    public static PurchaseOrderLineId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("PurchaseOrderLineId cannot be null or whitespace.");
        }

        return new PurchaseOrderLineId(value);
    }
}
