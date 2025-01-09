namespace RacksStand.Api.Features.Products;

public record ProductRequest
(
     string CategoryId,
     string Name,
     string? Description,
     string? Brand,
     string? Type,
     string? Tags,
     string? Metadata,
     IEnumerable<ProductOptionRequest>? ProductOptions,
     IEnumerable<ProductVariantRequest> ProductVariants
);

public record ProductOptionRequest
(
    string Name
);

public record ProductVariantOptionRequest
(
    string Name,
    string Value
);

public record ProductVariantRequest
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
    string? AttachmentId,
    IEnumerable<ProductVariantOptionRequest>? ProductVariantOptions
);
