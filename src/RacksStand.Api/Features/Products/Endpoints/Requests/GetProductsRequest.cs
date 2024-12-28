namespace RacksStand.Api.Features.Products.Endpoints.Requests;

public record GetProductsRequest
(
    [FromQuery] int pageSize,
    [FromQuery] int pageNumber,
    [FromQuery] string? filter
);
