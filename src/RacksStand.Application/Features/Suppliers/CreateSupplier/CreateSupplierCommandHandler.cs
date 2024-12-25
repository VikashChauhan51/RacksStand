using MediatorForge.CQRS.Commands;

namespace RacksStand.Application.Features.Suppliers.CreateSupplier;
public class CreateSupplierCommandHandler : ICommandHandler<CreateSupplierCommand, Result<Unit>>
{
    public Task<Result<Unit>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Handler not implemented");
    }
}
