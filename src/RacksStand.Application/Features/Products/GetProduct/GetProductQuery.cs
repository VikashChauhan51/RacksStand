using MediatorForge.CQRS.Queries;

namespace RacksStand.Application.Features.Products.GetProduct;
public record GetProductQuery(
    string Id
) : IQuery<Result<GetProductQueryResponse>>;

