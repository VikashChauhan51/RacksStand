using MediatorForge.CQRS.Queries;

namespace RacksStand.Application.Features.Suppliers.GetSupplier;
public record GetSupplierQuery(
    string Id
) : IQuery<Result<GetSupplierQueryResponse>>;

