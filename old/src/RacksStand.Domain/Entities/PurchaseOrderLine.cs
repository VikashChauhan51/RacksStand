using RacksStand.Domain.Core;
using RacksStand.Domain.Enums;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class PurchaseOrderLine : Entity<PurchaseOrderLineId>
{
    public required PurchaseOrderId PurchaseOrderId { get; set; }
    public required ProductVariantId ProductVariantId { get; set; }
    public required Quantity Quantity { get; set; }
    public required PurchasePrice UnitPrice { get; set; }
    public required CurrencyCode CurrencyCode { get; set; }
    public decimal TotalPrice => Quantity.Value * UnitPrice.Value;

    // Navigation properties
    public virtual PurchaseOrder PurchaseOrder { get; set; } = default!;
    public virtual ProductVariant ProductVariant { get; set; } = default!;
}

