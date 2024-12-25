namespace RacksStand.Models;
public record ProductModel(
    string Id,
    string Name,
    string? Description,
    string CategoryId,
    string? SupplierId,
    string Code,
    decimal PurchasePrice,
    decimal SellingPrice,
    string Unit
);
