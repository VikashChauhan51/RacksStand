using RacksStand.Domain.Core;
using RacksStand.Domain.Enums;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class ProductVariant : Entity<ProductVariantId>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Code { get; set; }
    public required string Sku { get; set; }
    public Barcode? Barcode { get; set; }
    public required PurchasePrice PurchasePrice { get; set; }
    public required SellingPrice SellingPrice { get; set; }
    public required Unit Unit { get; set; }
    public required Height Height { get; set; }
    public required Width Width { get; set; }
    public required Length Length { get; set; }
    public required DimensionUnit DiminsionUnit { get; set; }
    public required Weight Weight { get; set; }
    public required WeightUnit WeightUnit { get; set; }
    public required ProductId ProductId { get; set; }
    public SupplierId? SupplierId { get; set; }
    public AttachmentId? ImageId { get; set; }

    public virtual Product Product { get; set; } = default!;
    public virtual ICollection<ProductVariantOption> ProductVariantOptions { get; set; } = [];
}
