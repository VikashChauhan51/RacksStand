namespace RacksStand.Api.Features.Shops.Models;

public record ShopRequest
(
    string Name,
    string Address,
    string? Description
);
