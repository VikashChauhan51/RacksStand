using MediatorForge.CQRS.Queries;

namespace RacksStand.Application.Features.Pocket.GetPocket;
public class GetPocketQueryHandler : IQueryHandler<GetPocketQuery, Result<GetPocketQueryResponse>>
{
    public Task<Result<GetPocketQueryResponse>> Handle(GetPocketQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Handler not implemented");
    }
}

