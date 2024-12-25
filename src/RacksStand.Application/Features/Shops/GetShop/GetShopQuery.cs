using MediatorForge.CQRS.Queries;


namespace RacksStand.Application.Features.Shops.GetShop;
public record GetShopQuery(
    string Id
) : IQuery<Result<GetShopQueryResponse>>;

