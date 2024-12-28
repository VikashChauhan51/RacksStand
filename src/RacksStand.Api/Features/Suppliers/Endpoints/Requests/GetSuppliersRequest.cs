namespace RacksStand.Api.Features.Suppliers.Endpoints.Requests;

public record GetSuppliersRequest
(
    [FromQuery] int pageSize,
    [FromQuery] int pageNumber,
    [FromQuery] string? filter
);
