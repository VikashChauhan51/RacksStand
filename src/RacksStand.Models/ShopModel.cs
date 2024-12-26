namespace RacksStand.Models;
public record ShopModel(
    string Id,
    string Name,
    string? Description,
    string? Address,
    string OwnerId,
    List<UserShopModel> UserShops,
    List<RackModel> Racks
);

