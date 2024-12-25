using MediatorForge.CQRS.Commands;

namespace RacksStand.Application.Features.Suppliers.CreateSupplier;
public record CreateSupplierCommand(
    string Name,
    string Code
) : ICommand<Result<Unit>>;

