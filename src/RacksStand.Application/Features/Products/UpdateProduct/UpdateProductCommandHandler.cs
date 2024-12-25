using MediatorForge.CQRS.Commands;

namespace RacksStand.Application.Features.Products.UpdateProduct;
public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, Result<Unit>>
{
    public Task<Result<Unit>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Handler not implemented");
    }
}
