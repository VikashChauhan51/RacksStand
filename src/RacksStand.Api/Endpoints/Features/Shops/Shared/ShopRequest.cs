namespace RacksStand.Api.Endpoints.Features.Shops.Shared;

public record ShopRequest
(
    string Name,
    string Address,
    string? Description
);
