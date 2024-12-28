namespace RacksStand.Api.Endpoints.Features.Products.GetProducts;

public record GetProductsRequest
(
    [FromQuery] int pageSize,
    [FromQuery] int pageNumber,
    [FromQuery] string? filter
);
