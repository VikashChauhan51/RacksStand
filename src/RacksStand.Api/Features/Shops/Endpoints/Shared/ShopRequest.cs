namespace RacksStand.Api.Features.Shops.Endpoints.Shared;

public record ShopRequest
(
    string Name,
    string Address,
    string? Description
);
