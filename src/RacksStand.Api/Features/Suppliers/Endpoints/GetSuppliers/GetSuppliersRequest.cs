namespace RacksStand.Api.Features.Suppliers.Endpoints.GetSuppliers;

public record GetSuppliersRequest
(
    [FromQuery] int pageSize,
    [FromQuery] int pageNumber,
    [FromQuery] string? filter
);
