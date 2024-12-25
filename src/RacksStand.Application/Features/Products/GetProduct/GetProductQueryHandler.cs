using MediatorForge.CQRS.Queries;

namespace RacksStand.Application.Features.Products.GetProduct;
public class GetProductQueryHandler : IQueryHandler<GetProductQuery, Result<GetProductQueryResponse>>
{
    public Task<Result<GetProductQueryResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Handler not implemented");
    }
}

