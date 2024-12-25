using MediatorForge.CQRS.Commands;

namespace RacksStand.Application.Features.Products.CreateProduct;
public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Result<Unit>>
{
    public Task<Result<Unit>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Handler not implemented");
    }
}

