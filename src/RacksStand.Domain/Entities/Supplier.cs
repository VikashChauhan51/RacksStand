using RacksStand.Domain.Core;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class Supplier : Entity<SupplierId>
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public required CompanyId CompanyId { get; set; }
    public string? PrefererCommunication { get; set; }
    public string? Description { get; set; }
    public string? RepresentiveName { get; set; }
    public required string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public required string State { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public required string ZipCode { get; set; }
    public required string BillingAddressLine1 { get; set; }
    public string? BillingAddressLine2 { get; set; }
    public required string BillingState { get; set; }
    public required string BillingCity { get; set; }
    public required string BillingCountry { get; set; }
    public required string BillingZipCode { get; set; }

    // Navigation properties
    public virtual Company Company { get; set; } = default!;
    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = [];
    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = [];
}
