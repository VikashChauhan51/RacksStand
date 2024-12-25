using MediatorForge.CQRS.Queries;

namespace RacksStand.Application.Features.Shops.GetShop;
public class GetShopQueryHandler : IQueryHandler<GetShopQuery, Result<GetShopQueryResponse>>
{
    public async Task<Result<GetShopQueryResponse>> Handle(GetShopQuery request, CancellationToken cancellationToken)
    {
        return Result<GetShopQueryResponse>.Fail(new Exception("Shop not found."));
    }
}
