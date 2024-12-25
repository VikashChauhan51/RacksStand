using MediatorForge.CQRS.Queries;

namespace RacksStand.Application.Features.Racks.GetRack;
public record GetRackQuery(
    string Id
) : IQuery<Result<GetRackQueryResponse>>;
