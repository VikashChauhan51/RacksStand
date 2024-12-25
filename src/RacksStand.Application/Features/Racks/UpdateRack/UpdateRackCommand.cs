using MediatorForge.CQRS.Commands;

namespace RacksStand.Application.Features.Racks.UpdateRack;
public record UpdateRackCommand(
    string Id,
    string Name,
    string Code,
    string? Label,
    string? Description,
    string StoreId
) : ICommand<Result<Unit>>;

