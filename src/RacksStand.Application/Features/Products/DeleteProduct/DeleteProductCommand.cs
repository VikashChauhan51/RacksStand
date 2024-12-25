using MediatorForge.CQRS.Commands;

namespace RacksStand.Application.Features.Products.DeleteProduct;
public record DeleteProductCommand(
    string Id
) : ICommand<Result<Unit>>;
