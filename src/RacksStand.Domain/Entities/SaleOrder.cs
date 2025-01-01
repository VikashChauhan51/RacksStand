using RacksStand.Domain.Core;
using RacksStand.Domain.Enums;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class SaleOrder : Entity<SaleOrderId>
{
    public required string OrderNumber { get; set; }
    public required DateTime OrderDate { get; set; }
    public required CustomerId CustomerId { get; set; }
    public required WarehouseId WarehouseId { get; set; }
    public string? ShippingAddress { get; set; }
    public string? ShippingCity { get; set; }
    public string? ShippingState { get; set; }
    public string? ShippingCountry { get; set; }
    public string? ShippingPostalCode { get; set; }
    public SaleOrderStatus Status { get; set; } 
    public string? Notes { get; set; }

    // Navigation properties
    public virtual Customer Customer { get; set; } = default!;
    public virtual Warehouse Warehouse { get; set; } = default!;
    public virtual ICollection<SaleOrderLine> Lines { get; set; } = new List<SaleOrderLine>();
}

