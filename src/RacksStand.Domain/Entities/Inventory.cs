using RacksStand.Domain.Core;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class Inventory : Entity<InventoryId>
{
    public required ProductVariantId ProductVariantId { get; set; }
    public required BinId BinId { get; set; }
    public required Quantity Quantity { get; set; }
    public required WarehouseId WarehouseId { get; set; }
    public SupplierId? SupplierId { get; set; }

    // Navigation properties
    public virtual ProductVariant ProductVariant { get; set; } = default!;
    public virtual Warehouse Warehouse { get; set; } = default!;
    public virtual Bin Bin { get; set; } = default!;
    public virtual Supplier? Supplier { get; set; } = default!;
}

