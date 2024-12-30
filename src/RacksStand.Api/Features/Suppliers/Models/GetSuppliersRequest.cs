namespace RacksStand.Api.Features.Suppliers.Models;

public record GetSuppliersRequest
(
    [FromQuery] int pageSize,
    [FromQuery] int pageNumber,
    [FromQuery] string? filter
);
