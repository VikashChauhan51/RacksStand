using MediatorForge.CQRS.Commands;

namespace RacksStand.Application.Features.Suppliers.DeleteSupplier;
public record DeleteSupplierCommand(
    string Id
) : ICommand<Result<Unit>>;

