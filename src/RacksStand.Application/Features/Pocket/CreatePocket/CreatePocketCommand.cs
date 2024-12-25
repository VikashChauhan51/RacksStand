using MediatorForge.CQRS.Commands;
using RacksStand.Domain.Enums;

namespace RacksStand.Application.Features.Pocket.CreatePocket;
public record CreatePocketCommand(
    string RackRowId,
    string Code,
    int ColumnNumber,
    int? Capacity,
    PocketState State
) : ICommand<Result<Unit>>;

