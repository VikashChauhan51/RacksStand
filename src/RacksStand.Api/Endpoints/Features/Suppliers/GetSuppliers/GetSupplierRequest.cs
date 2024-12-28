namespace RacksStand.Api.Endpoints.Features.Suppliers.GetSuppliers;

public record GetSupplierRequest
(
    [FromQuery] int pageSize,
    [FromQuery] int pageNumber,
    [FromQuery] string? filter
);
