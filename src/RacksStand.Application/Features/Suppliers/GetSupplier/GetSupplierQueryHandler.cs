using MediatorForge.CQRS.Queries;

namespace RacksStand.Application.Features.Suppliers.GetSupplier;
public class GetSupplierQueryHandler : IQueryHandler<GetSupplierQuery, Result<GetSupplierQueryResponse>>
{
    public Task<Result<GetSupplierQueryResponse>> Handle(GetSupplierQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Handler not implemented");
    }
}
