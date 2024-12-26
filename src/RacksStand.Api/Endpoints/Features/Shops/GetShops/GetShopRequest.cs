namespace RacksStand.Api.Endpoints.Features.Shops.GetShops;

public record GetShopRequest
(
    [FromQuery] int pageSize,
    [FromQuery] int pageNumber,
    [FromQuery] string? filter
);
