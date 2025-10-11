namespace RacksStand.Api.Features.Products;

public record UpdateProductVariantRequest
(
    string Name,
    string? Description,
    string Code,
    string Sku,
    string? Barcode,
    string? BarcodeFormat,
    decimal PurchasePrice,
    decimal SellingPrice,
    string CurrencyCode,
    decimal Height,
    decimal Width,
    decimal Length,
    string DiminsionUnit,
    decimal Weight,
    string WeightUnit,
    string? SupplierId,
    string? AttachmentId
);
