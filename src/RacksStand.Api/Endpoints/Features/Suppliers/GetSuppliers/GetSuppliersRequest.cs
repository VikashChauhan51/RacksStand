namespace RacksStand.Api.Endpoints.Features.Suppliers.GetSuppliers;

public record GetSuppliersRequest
(
    [FromQuery] int pageSize,
    [FromQuery] int pageNumber,
    [FromQuery] string? filter
);
