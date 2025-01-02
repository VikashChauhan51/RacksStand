using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
 
public record PurchaseOrderLineId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private PurchaseOrderLineId(string value) => _value = value;
    public static PurchaseOrderLineId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("PurchaseOrderLineId cannot be null or whitespace.");
        }

        return new PurchaseOrderLineId(value);
    }
}
