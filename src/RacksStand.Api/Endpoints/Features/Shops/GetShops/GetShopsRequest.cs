namespace RacksStand.Api.Endpoints.Features.Shops.GetShops;

public record GetShopsRequest
(
    [FromQuery] int pageSize,
    [FromQuery] int pageNumber,
    [FromQuery] string? filter
);
