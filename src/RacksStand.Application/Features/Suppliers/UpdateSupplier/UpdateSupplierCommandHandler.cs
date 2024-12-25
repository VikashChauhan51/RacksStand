using MediatorForge.CQRS.Commands;

namespace RacksStand.Application.Features.Suppliers.UpdateSupplier;
public class UpdateSupplierCommandHandler : ICommandHandler<UpdateSupplierCommand, Result<Unit>>
{
    public Task<Result<Unit>> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Handler not implemented");
    }
}

