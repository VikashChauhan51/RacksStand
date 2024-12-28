namespace RacksStand.Api.Features.Products.Endpoints.Requests;

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
