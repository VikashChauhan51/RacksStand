namespace RacksStand.Api.Features.Products.Models;

public record GetProductsRequest
(
    [FromQuery] int pageSize,
    [FromQuery] int pageNumber,
    [FromQuery] string? filter
);
