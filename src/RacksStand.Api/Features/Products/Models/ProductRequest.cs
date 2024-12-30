namespace RacksStand.Api.Features.Products.Models;

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
