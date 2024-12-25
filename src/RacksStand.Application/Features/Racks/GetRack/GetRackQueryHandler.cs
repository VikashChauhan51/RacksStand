using MediatorForge.CQRS.Queries;


namespace RacksStand.Application.Features.Racks.GetRack;
public class GetRackQueryHandler : IQueryHandler<GetRackQuery, Result<GetRackQueryResponse>>
{
    public Task<Result<GetRackQueryResponse>> Handle(GetRackQuery request, CancellationToken cancellationToken)
    {
         throw new NotImplementedException();
    }
}

