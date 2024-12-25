using MediatorForge.CQRS.Commands;

namespace RacksStand.Application.Features.Suppliers.UpdateSupplier;
public record UpdateSupplierCommand(
    string Id,
    string Name,
    string Code
) : ICommand<Result<Unit>>;

