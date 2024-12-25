using MediatorForge.CQRS.Commands;

namespace RacksStand.Application.Features.Products.CreateProduct;
public record CreateProductCommand(
    string Name,
    string? Description,
    string CategoryId,
    string? SupplierId,
    string Code,
    decimal PurchasePrice,
    decimal SellingPrice,
    string Unit
) : ICommand<Result<Unit>>;

