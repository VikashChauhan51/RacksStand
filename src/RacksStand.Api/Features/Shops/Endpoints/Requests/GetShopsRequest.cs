namespace RacksStand.Api.Features.Shops.Endpoints.Requests;

public record GetShopsRequest
(
    [FromQuery] int pageSize,
    [FromQuery] int pageNumber,
    [FromQuery] string? filter
);
