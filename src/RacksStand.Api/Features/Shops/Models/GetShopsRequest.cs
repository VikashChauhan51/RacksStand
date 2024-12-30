namespace RacksStand.Api.Features.Shops.Models;

public record GetShopsRequest
(
    [FromQuery] int pageSize,
    [FromQuery] int pageNumber,
    [FromQuery] string? filter
);
