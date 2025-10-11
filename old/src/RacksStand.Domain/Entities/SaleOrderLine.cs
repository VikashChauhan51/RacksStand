using RacksStand.Domain.Core;
using RacksStand.Domain.Enums;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class SaleOrderLine : Entity<SaleOrderLineId>
{
    public required SaleOrderId SaleOrderId { get; set; }
    public required ProductVariantId ProductVariantId { get; set; }
    public required Quantity Quantity { get; set; }
    public required SellingPrice UnitPrice { get; set; }
    public required CurrencyCode CurrencyCode { get; set; }
    public decimal TotalPrice => Quantity.Value * UnitPrice.Value;

    // Navigation properties
    public virtual SaleOrder SaleOrder { get; set; } = default!;
    public virtual ProductVariant ProductVariant { get; set; } = default!;
}


