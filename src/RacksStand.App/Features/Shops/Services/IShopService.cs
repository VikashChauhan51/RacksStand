using RacksStand.App.Features.Shops.Models;
using Refit;

namespace RacksStand.App.Features.Shops.Services;

public interface IShopService
{
    [Get("/users/{user}")]
    Task<ShopModel> GetUser(string user);
}
