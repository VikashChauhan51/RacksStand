using MediatorForge.CQRS.Commands;
using RacksStand.Domain.Enums;

namespace RacksStand.Application.Features.Pocket.UpdatePocket;
public record UpdatePocketCommand(
    string Id,
    string RackRowId,
    string Code,
    int ColumnNumber,
    int? Capacity,
    PocketState State
) : ICommand<Result<Unit>>;

