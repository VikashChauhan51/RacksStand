namespace RacksStand.Api.Endpoints.Features.Products.Shared;

public record ProductRequest(
    string Name,
    string? Description,
    string? SupplierId,
    string Code,
    string Sku,
    decimal PurchasePrice,
    decimal SellingPrice,
    string Unit
);
