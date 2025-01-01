using RacksStand.Domain.Core;
using RacksStand.Domain.Enums;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class PurchaseOrder : Entity<PurchaseOrderId>
{
    public required string OrderNumber { get; set; }
    public required DateTime OrderDate { get; set; }
    public required SupplierId SupplierId { get; set; }
    public required WarehouseId WarehouseId { get; set; }
    public string? DeliveryAddress { get; set; }
    public string? DeliveryCity { get; set; }
    public string? DeliveryState { get; set; }
    public string? DeliveryCountry { get; set; }
    public string? DeliveryPostalCode { get; set; }
    public PurchaseOrderStatus Status { get; set; } 
    public string? Notes { get; set; }

    // Navigation properties
    public virtual Supplier Supplier { get; set; } = default!;
    public virtual Warehouse Warehouse { get; set; } = default!;
    public virtual ICollection<PurchaseOrderLine> Lines { get; set; } =[];
}

