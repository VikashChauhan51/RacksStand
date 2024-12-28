namespace RacksStand.Api.Features.Shops.Endpoints.Requests;

public record ShopRequest
(
    string Name,
    string Address,
    string? Description
);
