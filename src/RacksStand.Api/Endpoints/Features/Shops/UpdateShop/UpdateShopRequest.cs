namespace RacksStand.Api.Endpoints.Features.Shops.UpdateShop;

public record UpdateShopRequest
(
    string Name,
    string Address,
    string? Description
);
