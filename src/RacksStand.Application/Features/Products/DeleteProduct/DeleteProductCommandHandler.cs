using MediatorForge.CQRS.Commands;

namespace RacksStand.Application.Features.Products.DeleteProduct;
public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, Result<Unit>>
{
    public Task<Result<Unit>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Handler not implemented");
    }
}
