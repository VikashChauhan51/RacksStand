namespace RacksStand.Api.Endpoints.Features.Shops.CreateShop;
public record CreateShopRequest
(
    string Name,
    string Address,
    string? Description
);
