namespace RacksStand.Api.Features.Products.Endpoints.GetProducts;

public record GetProductsRequest
(
    [FromQuery] int pageSize,
    [FromQuery] int pageNumber,
    [FromQuery] string? filter
);
