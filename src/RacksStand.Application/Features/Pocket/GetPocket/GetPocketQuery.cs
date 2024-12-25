using MediatorForge.CQRS.Queries;

namespace RacksStand.Application.Features.Pocket.GetPocket;
public record GetPocketQuery(
    string Id
) : IQuery<Result<GetPocketQueryResponse>>;

