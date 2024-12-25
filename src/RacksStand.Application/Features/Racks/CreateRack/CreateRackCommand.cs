using MediatorForge.CQRS.Commands;

namespace RacksStand.Application.Features.Racks.CreateRack;
public record CreateRackCommand(
    string Name,
    string Code,
    string? Label,
    string? Description,
    string StoreId
) : ICommand<Result<Unit>>;

