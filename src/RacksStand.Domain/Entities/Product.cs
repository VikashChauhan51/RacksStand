using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class Product : Entity<ProductId>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required CategoryId CategoryId { get; set; }
    public SupplierId? SupplierId { get; set; }
    public required string Code { get; set; }
    public required Price PurchasePrice { get; set; }
    public required Price SellingPrice { get; set; }
    public required Unit Unit { get; set; }

    // Navigation properties
    public virtual Category Category { get; set; } = default!;
    public virtual Supplier? Supplier { get; set; }
    public virtual ICollection<PocketItem> PocketItems { get; set; } = [];

}
