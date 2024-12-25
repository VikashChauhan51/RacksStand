using MediatorForge.CQRS.Commands;

namespace RacksStand.Application.Features.Suppliers.DeleteSupplier;
public class DeleteSupplierCommandHandler : ICommandHandler<DeleteSupplierCommand, Result<Unit>>
{
    public Task<Result<Unit>> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Handler not implemented");
    }
}
