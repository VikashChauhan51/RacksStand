namespace RacksStand.Api.Features.Shops.Endpoints.GetShops;

public record GetShopsRequest
(
    [FromQuery] int pageSize,
    [FromQuery] int pageNumber,
    [FromQuery] string? filter
);
